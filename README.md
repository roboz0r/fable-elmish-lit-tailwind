# Fable Elmish Lit Tailwind

A template for getting started with F# & Fable for web development.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js 20 LTS](https://nodejs.org/)

## Getting Started

These commands will install the JavaScript and .NET dependencies.
You only need to run these commands when you first start or when you update your dependencies.

```pwsh
npm install
dotnet tool restore
dotnet paket install
```

## Everyday development

The below command starts the development server in watch mode.

```pwsh
npm run dev
# Note: To stop the dev server press Ctrl+C in the terminal
```

It starts 3 processes in parallel:

- [Fable](https://fable.io), the F# to JavaScript compiler, will watch your F# files and recompile them automatically into the [build](./build) directory.
- [Tailwind CSS](https://tailwindcss.com) provides a set of utility styles. In development mode, it will watch your code and recompile the CSS files as you add more classes.
- [Vite](https://vitejs.dev/), JavaScript build tooling and development server. It watches the build directory and reloads the pages when the JavaScript changes.

By default, it will start [here](http://localhost:5173/).

## Building for production

The below command builds and bundles your F# and CSS into minified JavaScript in the [dist](./dist) directory.
Any files in the [public](./public) directory are also copied to dist.

```pwsh
npm run build
```

## Understanding the config files

The following configuration files are used to support this project:

- [.editorconfig](.editorconfig) Provides configuration settings for [Fantomas](https://fsprojects.github.io/fantomas/) the F# code formatter.
- [.gitignore](.gitignore) Defines files that should be ignored by Git
- [global.json](global.json) Defines the required version of the .NET SDK.
- [package.json](package.json) Defines the JavaScript dependencies from [npm](https://www.npmjs.com/) and scripts started with the `npm` command.
- [paket.dependencies](paket.dependencies) Defines the .NET dependencies from [NuGet](https://www.nuget.org/). [Paket](https://fsprojects.github.io/Paket/) centralizes version management for your .NET dependencies.
  - Note: The [paket.lock](paket.lock) file lists the resolved dependencies.
  - Note: Each fsproj needs a corresponding [paket.references](/src/paket.references) to select which dependencies are required for that project.
- [tailwind.config.cjs](tailwind.config.cjs) Defines the Tailwind CSS compiler and plugin settings.
- [tsconfig.json](tsconfig.json) Defines TypeScript compiler settings.
- [vite.config.ts](vite.config.ts) Defines Vite's compiler and plugin settings.
- [dotnet-tools.json](.config/dotnet-tools.json) Defines the .NET tools for this project.
