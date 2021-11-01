# Flea
FLEA - Flea Location EstablishingApplication

# Development environment

 - Net 5.0

# Development guidelines

 - New features are developed on separate feature branches.
 - All merges into master must happen through PRs.
 - A PR must be reviewed and approved by at least two people before merge into master.
 - Commit messages must describe the changes that have been made.

## Review guidelines

 - Check that the PR upholds naming conventions and commenting guidelines.
 - Code must be sufficiently tested. Either through manuel test (this must be thoughroughly documented) or automated unit or integration tests.
 - Check for code redundancy. Redundant code should generally be seperated into smaller functions.
 - Files must be organised into the proper folders according to the folder organisation guidelines.

## Folder organisation

 TODO

# Style guide

 - Larger functions must have a comment summarising its functionality.
 - We'd rather have too many comments than too few.
 - We use the official C# naming convention (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
 - We use project wide nullable reference types. 
