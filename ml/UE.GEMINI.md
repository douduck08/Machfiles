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
- **Build Skill**: Use `ue-build` for all C++ compilation tasks.
- **Build Workflow**: Run `build_and_analyze.py` and prioritize `analyzed_build.md` for error recovery.
- **Testing Skill**: Use `ue-test-pure-function` for implementing and running automated tests.
- **Fuzzy Testing**: Implement randomized tests with seed logging and `-seed` parameter support for reproducibility.
- **CLI Execution**: Always use `-unattended` to prevent hangs and `-NullRHI` for faster, headless execution.
- **Manual Check**: User can launch Editor via `<ProjectName>/launch_editor.bat`.

## Workflow Orchestration
### 0. Session Startup (MANDATORY)
When starting a new conversation about this project:
1. Read `docs/todo.md` to identify in-progress tasks and their `🔄 Handoff` blocks.
2. Read `docs/lessons.md` to load known pitfalls.
3. For the relevant plugin(s), read their `docs/README.md`.

### 1. Planning First
- All plans require user review before execution.
- If the implementation deviates significantly, STOP and re-plan immediately.
- Use `ue-task-manager` skill for task tracking format and handoff protocol.

### 2. Task Lifecycle
For every task, follow this sequence:
1. **Plan**: Write the implementation plan to `docs/todo.md`.
2. **Verify Plan**: Obtain user approval before starting.
3. **Track**: Update checkboxes in `docs/todo.md` as steps complete.
4. **Compile**: Recompile after every significant modification. Use `ue-build` skill.
5. **Document**: Update plugin `docs/README.md` to reflect changes.
6. **Capture Lessons**: Update `docs/lessons.md` after resolving issues.

### 3. Self-Improvement Loop
- Log all corrections in `docs/lessons.md`.
- Use `ue-lesson-tracker` skill for recording format and retrieval guidelines.
- Review lessons after every fix to prevent recurring errors.

### 4. Compilation & Recovery
- Recompile after every significant modification. Use `ue-build` skill.
- Identify root causes before modifying. No trial-and-error fixes.
- Maximum 5 retry attempts per error. If unresolved, stop and report.

### 5. Verification
- No task is complete without verification.
- Aim for "Staff Engineer" quality standards.
- Include at least one automated validation or clear manual testing steps.

### 6. Persistent Documentation
- Every plugin must maintain a `docs/README.md`.
- Continuously update with architecture, features, and implementation logic.
- **Relative Paths**: All file links in documentation must use relative paths.

### 7. Version Control
- Git commits only when explicitly requested by the user.
- Every commit message must include `[AI Generated]` and a concise summary.

## Core Principles
- **Simplicity First**: Ensure changes are as simple as possible with minimal code impact.
- **No Laziness**: Focus on root causes. No temporary fixes. Maintain senior developer standards.
- **Minimal Impact**: Modify only what is necessary to avoid introducing regression bugs.
