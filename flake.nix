{
  description = "Messaging Service";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
        pname = "core-service";
        version = "1.0.0";

        src = ./.;

        dotnetSdk = pkgs.dotnet-sdk_9;
        dotnetRuntime = pkgs.dotnet-runtime_9;

        mainProjectName = "BeatEcoprove.Api";
      in
      {
        packages = {
          default = pkgs.buildDotnetModule {
            inherit version src;

            pname = mainProjectName;
            projectFile = "src/${mainProjectName}/${mainProjectName}.csproj";
            nugetDeps = ./deps.json;

            dotnet-sdk = dotnetSdk;
            dotnet-runtime = dotnetRuntime;

            buildType = "Release";
            selfContainedBuild = true;

            executables = [ mainProjectName ];
          };

          docker = pkgs.dockerTools.buildLayeredImage {
            name = pname;
            tag = version;
            contents = [
              self.packages.${system}.default
              pkgs.cacert
              pkgs.bash
              pkgs.coreutils
            ];
            config = {
              Cmd = [ "${self.packages.${system}.default}/bin/${pname}" "start" ];
              Env = [
                "SSL_CERT_FILE=${pkgs.cacert}/etc/ssl/certs/ca-bundle.crt"
                "LANG=C.UTF-8"
              ];
            };
          };
        };

        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnetSdk
          ];

          shellHook = ''

            '';
        };

      });
}
