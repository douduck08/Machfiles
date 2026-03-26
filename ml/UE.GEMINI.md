# Project: Vibe

## Project Overview
An experimental Unreal Engine project for validating AI Agent development workflows.

### Project Identification
- **Project Name**: Vibe
- **Prefix**: Vibe (Primary class prefix)

### Key Technologies
- **Unreal Engine**: 5.7.3-release
- **IDE**: Visual Studio 2022
- **Standard**: C++20

### Project Structure
- **Engine**: Source code of Unreal Engine.
- **<ProjectName>**: Main game project root.
    - `Source/<ProjectName>`: Main module source.
        - `Public/**/*.h`, `Private/**/*.cpp`, `<ProjectName>.Build.cs`.
    - `Plugins/**/<PluginName>`: Custom project-specific plugins.
        - `Source/<PluginName>`: Plugin source root.
            - `Public/**/*.h`, `Private/**/*.cpp`, `<PluginName>.Build.cs`.
        - `<PluginName>.uplugin`: Plugin configuration file.
        - `docs`: Plugin-specific documentation.
    - `docs`: Main project documentation.
    - `<ProjectName>.uproject`: Project configuration file.

### Coding Style
#### General Naming Conventions
- **Language**: All identifiers must be in English.
- **Casing**: PascalCase for all files, folders, and identifiers (no underscores).
- **Singularity**: Use singular nouns only.

#### Class Naming
- **Prefixes**: Mandatory Unreal prefixes (`A`, `U`, `F`).
- **Main Source Scope**: Prefix classes in `Source/<ProjectName>` with `<Prefix>`.
- **Plugin Scope**: Prefix classes in `Plugins/<PluginName>` with the `<PluginName>` (or abbreviation).

#### Naming Examples
*(Note: Examples below assume ProjectName="Vibe", Prefix="Vibe")*
```cpp
// Path: Vibe/Source/Vibe/Public/<ModuleName>/VibeCharacter.h
class AVibeCharacter : public ACharacter {};

// Path: Vibe/Source/Vibe/Public/<ModuleName>/VibePlayerData.h
class UVibePlayerData : public UObject {};

// Path: Vibe/Source/Vibe/Public/<ModuleName>/VibeData.h
struct FVibeData {};
```

#### Function Naming
- **Parameters**: 
    - Use `const` for primitives (built-in types).
    - Use `const&` for Structs and `TArray` for read-only inputs.
    - Use `&` with `Out` or `InOut` prefixes for output/mutable parameters.

#### Variable Naming
- **Booleans**: Prefix with `b` (e.g., `bIsActive`).

#### File & Directory Naming
- **Files**: Filenames must match the class name (excluding prefixes `A`, `U`, `F`).
- **Organization**: All source files must be categorized into `<ModuleName>` folders under `Public/Private`.
- **Depth**: Maximum one level of subdirectories under `<ModuleName>`.
- **Global Types**: Shared structs should be grouped in `<ModuleName>Types.h`.
- **Shared Folders**: Use the `Base` directory for cross-module shared logic (e.g., `Public/Base/`).

### For Unreal Engine Project
- **Headers**: `#include "Filename.generated.h"` must be the final include.
- **Interfaces**: Apply the `I` prefix and include the corresponding `U` class declaration in the same file.
- **Logic**: 100% C++ logic implementation. No logic in Blueprints.
- **Gameplay**: Expose core functions as `BlueprintCallable` for game design use.
- **UI**: Use C++ Binding mechanisms for User Widgets (`WBP`).

### Building and Verification
- **Skill**: Use the `unreal_build` skill for all C++ compilation tasks.
- **Workflow**: Run `build_and_analyze.py` and prioritize `analyzed_build.md` for error recovery.
- **Manual Check**: User can launch Editor via `<ProjectName>/launch_editor.bat`.

## Workflow Orchestration
### 1. Planning
- All plans require user review before execution.
- If the implementation deviates significantly, STOP and re-plan immediately.

### 2. Self-Improvement Loop
- Log all user-requested corrections in `docs/lessons.md`.
- Document rules and examples to prevent recurring errors.
- Review `docs/lessons.md` after every fix for potential refinements.

### 3. Compilation & Recovery
- Recompile after every significant modification.
- If compilation fails, resolve the error before proceeding.
- **Root Cause Analysis**: Identify the fundamental cause of errors before modifying. Avoid trial-and-error fixes.
- **Retry Limit**: Maximum 5 attempts per error. If unresolved, stop and report to the user.

### 4. Verification
- No task is complete without verification.
- Aim for "Staff Engineer" quality standards.
- Include at least one automated validation or clear manual testing steps.

### 5. Persistent Documentation
- **Plugin Context**: Every plugin must maintain a `docs/README.md`.
- **Content**: Continuously update this file with the plugin's architecture, core features, and implementation logic to provide persistent context for both AI and human collaborators and prevent knowledge loss.

### 6. Version Control
- **Git Commits**: When explicitly requested by the user, commit changes to the repository.
- **Commit Message**: Every commit message must include the tag `[AI Generated]` and a concise summary of the work.

## Task Management
1. **Plan First**: Write the implementation plan to `docs/todo.md`.
2. **Verify Plan**: Obtain user approval before starting.
3. **Track Progress**: Update status using checkboxes in `docs/todo.md`.
4. **Explain Changes**: Provide a high-level summary for each step.
5. **Document Results**: Add a review section to `docs/todo.md` upon completion.
6. **Update Persistent Docs**: Update plugin-specific `docs/README.md` to reflect new architecture/features.
7. **Capture Lessons**: Update `docs/lessons.md` after resolving issues.

## Core Principles
- **Simplicity First**: Ensure changes are as simple as possible with minimal code impact.
- **No Laziness**: Focus on root causes. No temporary fixes. Maintain senior developer standards.
- **Minimal Impact**: Modify only what is necessary to avoid introducing regression bugs.
