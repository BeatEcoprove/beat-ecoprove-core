set shell := ["bash", "-c"]

default:
    @just --list

project_name := "BeatEcoprove.Api"
migration_project := "BeatEcoprove.Infrastructure"
solution := "BeatEcoprove.sln"

# Run the API locally
serve:
    dotnet run --project src/{{project_name}}

# Build the solution
build:
    dotnet build {{solution}}

# Build in Release mode
build-release:
    dotnet build {{solution}} -c Release

# Clean build artifacts
clean:
    dotnet clean {{solution}}
    
# Format project code
format:
    dotnet format
    
# Push migrations to database
migration-push:
   dotnet ef database update --startup-project src/{{project_name}} --project src/{{migration_project}}
   
# Rollback all migrations
migration-reset:
   dotnet ef database update 0 --startup-project src/{{project_name}} --project src/{{migration_project}} 
   
# Add a new migration
migration-create name:
    dotnet ef migrations add {{name}} --startup-project src/{{project_name}} --project src/{{migration_project}}
    
# Remove Migration
migration-remove:
    dotnet ef migrations remove --startup-project src/{{project_name}} --project src/{{migration_project}}
    
#List all migrations
migration-list:
    dotnet ef migrations list --startup-project src/{{project_name}} --project src/{{migration_project}}
