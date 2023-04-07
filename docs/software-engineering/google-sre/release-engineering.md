# Release Engineering

Running reliable services requires reliable release processes. Release engineers have a solid understanding of source code management, compilers, build configuration languages, automated build tools, package managers, and installers. Their skill set includes deep knowledge of multiple domains: development, configuration management, test integration, system administration, and customer support.

- Self-service model: In order to work at scale, teams must be self-sufficient. Release engineering has developed best practices and tools that allow the product development teams to control and run their own release processes. Each individual teams can decide how often and when to release new versions of their products.
- High velocity: Frequent releases result in fewer changes between versions, thus makes testing and troubleshooting easier.
- Hermetic build: Build tools must be insensitive to the libraries and other software installed on the build machine. Instead, builds depend on known versions of build tools, such as compilers, and dependencies, such as libraries. If two people attempt to build the same product at the same revision number in the source code repository on different machines, the result should be identical.
- Enforcement of Policies and Procedures: Several layers of security and access control determine who can perform specific operations when releasing a project. Almost all changes to the codebase require a code review, which is a streamlined action integrated into the normal developer workflow.

## Continuous Build and Deployment

Google has developed an automated release system called Rapid. Rapid is a system that leverages a number of Google technologies to provide a framework that delivers scalable, hermetic, and reliable releases.

- Building: Bazel supports building binaries from a range of languages. Engineers use Blaze to define build targets (e.g., the output of a build, such as a JAR file), and to specify the dependencies for each target. Build targets for binaries and unit tests are defined in Rapid's project configuration files. Project-specific flags, such as a unique build identifier, are passed by Rapid to Blaze.
- Branching: All code is checked into the main branch of the source code tree. However, the build tools branch from the mainline at a specific revision and never merge changes from the branch back into the mainline.
- Testing: The continuous test system runs unit tests against the code in the mainline each time a change is submitted to detect build and test failures. Release engineering recommends that the continuous build test targets correspond to the same test targets that gate the project release.
- Packaging: Software is distributed to the production machines via the Midas Package Manager. MPM assembles packages based on Blaze rules that list the build artifacts to include, along with their owners and permissions. MPM supports applying labels to a particular version of a package. Rapid applies a label containing the build ID, which guarantees that a package can be uniquely referenced using the name of the package and this label.

## Rapid

Rapid is configured with files called blueprints. Blueprints are written in an internal configuration language and are used to define build and test targets, rules for deployment, and administrative information. Role-based access control lists determine who can perform specific actions on a Rapid project. Each Rapid project has workflows that define the actions to perform during the release process.

- Rapid uses the requested integration revision number to create a release branch.
- Rapid uses Blaze to compile all the binaries and execute the unit tests. Compilation and testing occur in environments dedicated to those specific tasks, as opposed to taking place in the Borg job where the Rapid workflow is executing.
- Build artifacts are available for system testing and canary deployments.
- The results of each step of the process are logged. The report of all changes since the last release is created.

## Configuration Management

- Use the mainline for configuration: The method is used to configure services in Borg. Using this scheme, developers and SREs modify configuration files at the head of the main branch. The changes are reviewed and then applied to the running system. This technique often leads to skew between the checked-in version of the configuration files and the running version of the configuration file because jobs must be updated in order to pick up the changes.
- Include configuration files and binaries in the same MPM package: For projects with few configuration files or projects where the files change with each release cycle, the configuration files can be included in the MPM package with the binaries.
- Package configuration files into MPM "configuration packages": Binary configurations tend to be tightly bound to particular versions of binaries, thus Google leverages the build and packaging systems to snapshot and release configuration files alongside their binaries.
- Read configuration files from an external store: The configuration files could be stored in Chubby, Bigtable, or other source-based file system.
