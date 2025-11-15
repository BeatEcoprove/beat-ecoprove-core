{
  description = "Messaging Service";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils, ... }:
    let
      readVersion = { versionPath, fallback }:
        if builtins.pathExists versionPath
        then builtins.replaceStrings ["\n"] [""] (builtins.readFile versionPath)
        else fallback;
    in
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
        pname = "core-service";

        version = readVersion {
          versionPath = ./VERSION;
          fallback = "latest";
        };

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
              Cmd = [ "${self.packages.${system}.default}/bin/${mainProjectName}" ];
              Env = [
                "SSL_CERT_FILE=${pkgs.cacert}/etc/ssl/certs/ca-bundle.crt"
                "LANG=C.UTF-8"

                "DOTNET_RUNNING_IN_CONTAINER=true"
              ];
            };
          };
        };

        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnetSdk
            dotnet-ef
            just
          ];

          shellHook = ''
            echo "Development environment loaded"
            dotnet --version
          '';
        };

      });
}
