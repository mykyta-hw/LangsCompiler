**LangsCompiler** — is a compiler as an environment and a set of tools designed to create and process countless programming languages, as well as projects based on these languages. It provides specific but simple rules that interact to create complex and powerful behavior, allowing the implementation of languages ​​with unique capabilities.

### 💡 Project
**LangsCompiler** - is a comprehensive, self-sufficient platform built from scratch. Its goal is to go beyond the traditional compiler and become a full-fledged environment for designing, implementing languages, and developing projects based on them. I offer a set of rules and tools that simplify the creation of any language mechanisms.
Due to the novelty of the concept and the lack of ready-made solutions, the project deliberately avoids using redundant third-party libraries and technologies. Instead, the focus is on implementing specific algorithms and technologies from scratch. This approach not only allows you to avoid the "dead weight" of redundant functionality, but also promotes a deep understanding of the internal mechanisms of all algorithms and technologies used. At the current level of the project, I have already studied and mastered much more than could be obtained from traditional courses or dry theory.
The project grows with my experience and knowledge. The realization that design and architectural thinking are critically important has come with experience. Previously, I was inclined to immediately write code, but now I clearly see how careful design opens up new possibilities and prevents wasted time on rewriting.

### ✨ Features
LangsCompiler offers a number of unique features that simplify and extend the process of creating and compiling languages:
 * **Full compilation flexibility**: Allows you to easily describe the language syntax, perform complex logical and semantic checks, and generate any desired output - from JSON to machine code or custom bytecode.
 * **Language Interoperability**: During compilation, languages ​​can seamlessly communicate and exchange information, opening the way for hybrid language solutions.
 * **Intuitive build configuration**: Allows you to easily describe data flows and determine which languages ​​participate in the compilation process and at what stage.
 * **Rule sets**: Each rule set specializes in its own area (e.g. syntax, semantics), which significantly improves the readability and simplicity of the code, and also speeds up the process of creating a new language compared to the scripted approach.
 * **Easy project management**: You don't have to worry about complicated file manipulations or configuration nuances. Just add a folder with language files to your project, and the compiler will automatically find, analyze and compile everything.
 * **Freedom of modification**: All files describing the language are stored in text format, which ensures complete transparency. You can easily study and modify any aspect of the language.
 * **Automated Error Handling System**: Thanks to the concept of rule sets, much of the work involved in detecting and reporting errors is automated and standardized, making it much easier to create a new language.
 
### 🏛️ Architecture
LangsCompiler has strictly defined execution stages, ensuring a controlled and predictable compilation process. This allows for high modularity and scalability.
 * **Parsing**
   * Task: Search, analyze and create internal representations of all defined languages.
   * Search and identify files of all defined languages ​​in the project.
   * Analysis of the contents of these files (description of rules, configurations).
   * Creation of internal templates and language models for further use in the compilation process.
 * **Build Configuration**
   * Task: Defining the sequence and parameters for executing compilation steps.
   * Searching and analyzing build configuration files.
   * Creating a detailed sequence for executing compilation steps, taking into account all arguments and dependencies.
 * **Start Compilation**
   * Task: Initialize languages ​​and manage their execution according to the configuration.
   * Start execution of the generated build configuration.
   * Initialize language instances based on the created templates.
   * Combine languages ​​into logical groups for parallel or sequential processing.
   * Direct input data to the corresponding languages ​​and start their processing within the group.
 * **Language Processing**
   * Task: Process source code step by step within each language.
   * Execute syntax rules on the input data stream.
   * Execute semantic rules on the input data stream to check logic and consistency.
   * Create an Intermediate Representation (IR) based on the processed input data.
   * Execute code generation rules using the created IR to produce the target output.
   * Transform the generated files or resources into a unified data stream for passing to the next stage or storing.
 * **Finalization**
   * Task: Finalize the compilation process and release resources.
   * Save the final data stream to files and write them to the specified output folder (output or user).
   * Release all occupied resources.
   * Clear internal caches and temporary data.
   
### 📈 Roadmap & Status
The project is currently in an intensive architectural design phase. However, small code experiments may be conducted to test concepts or as a break from the main design work.
Current Priorities:
 * Fundamental Architecture Development: Designing the core structure of LangsCompiler to ensure its universality and scalability.
 * Precise Rule Description: Detailing the formal description of each rule type (syntax, semantics, generation) and their interactions.
Future Plans:
 * Cross-Platform and Portability: Ensuring the compiler can run on various operating systems and platforms for the easiest possible implementation in complex systems.
 * External APIs: Developing stable APIs for LangsCompiler to interact with other compilers or programs during the compilation process.
 * Integrated Development Environment (IDE): Creating a specialized IDE that will simplify and make more convenient the process of writing and debugging new languages ​​on the LangsCompiler platform.
 * Visualization of the Compilation Process: Implementation of mechanisms for visual tracking of each action, detail or overall picture of the compilation process, which will significantly improve debugging and understanding of the language.
 
### ⚙️ Build & Run
LangsCompiler is a regular console application without external dependencies, which guarantees ease of assembly.
Requirements:
 * .NET SDK 9.0 (or high).
 
Clone repository:
```
git clone https://github.com/-hw/LangsCompiler.git
cd LangsCompiler
```

Build compiler:
```
dotnet build
```

After successful assembly, the executable file of the compiler and the files necessary for its operation will appear in the `output` folder.
Important: The `localization` and `documentation` folders from the source code must be manually copied to the same directory where the executable file of the compiler is located.

Usage Example (Compilation).
To start compiling your project, use the command in the terminal:
```
lc build /path/to/your/project/file.proj
```

### Docs & helps:
You can get more detailed information on the use and capabilities of LangsCompiler from the built-in documentation by calling it with the commands:
```
lc docs
```
For information on a specific topic:
```
lc docs key-page
```

### 📄 License
This project is distributed under the MIT License. For more information, see the [LICENSE](LICENSE) file.
