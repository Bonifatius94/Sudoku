
## About
This is a project that deals with algorithmic issues when trying to solve / generate sudoku puzzles.
Those algorithmic features are then used by a small C# WPF Desktop App for demonstration purposes.

## Disclaimer
As this piece of code is quite boldly written, I'm using the opportunity to rehearse TDD and
refactoring on this codebase. So, the code should still work, but there will be some additional
testing / redesign efforts, to make this a nicer project.

## Build + Test
For building the project either use the standard way of doing things with .NET + Visual Studio or
use the 'dotnet' CLI when developing on Linux. The 'dotnet' commands are already provided as Dockerfile.

```sh
docker build . -t "sudoku"
docker run --name my-sudoku sudoku bash
docker cp my-sudoku:/app/out .
```

## License
This software is available under the terms of the MIT license.
