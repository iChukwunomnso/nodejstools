// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.IO;
using System.Threading.Tasks;
using Microsoft.NodejsTools.Npm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NpmTests
{
    [TestClass]
    public class InstallUninstallPackageTests : AbstractPackageJsonTests
    {
        [TestMethod, Priority(0), TestCategory("Ignore")]
        public void AddPackageToSimplePackageJsonThenUninstall()
        {
            using (var manager = new TemporaryFileManager())
            {
                var rootDir = FilesystemPackageJsonTestHelpers.CreateRootPackage(manager, PkgSimple);
                var controller = NpmControllerFactory.Create(rootDir, string.Empty);
                controller.Refresh();
                var rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package with no dependencies should not be null.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules before package install.");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.InstallPackageByVersionAsync("sax", "*", DependencyType.Standard, true);
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package installed.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package installed.");
                Assert.AreEqual(1, rootPackage.Modules.Count, "Should be one module after package installed.");

                var module = controller.RootPackage.Modules["sax"];
                Assert.IsNotNull(module, "Installed package should not be null.");
                Assert.AreEqual("sax", module.Name, "Module name mismatch.");
                Assert.IsNotNull(module.PackageJson, "Module package.json should not be null.");
                Assert.IsTrue(
                    module.IsListedInParentPackageJson,
                    "Should be listed as a dependency in parent package.json.");
                Assert.IsFalse(module.IsMissing, "Should not be marked as missing.");
                Assert.IsFalse(module.IsDevDependency, "Should not be marked as dev dependency.");
                Assert.IsFalse(module.IsOptionalDependency, "Should not be marked as optional dependency.");
                Assert.IsFalse(module.IsBundledDependency, "Should not be marked as bundled dependency.");
                Assert.IsTrue(module.HasPackageJson, "Module should have its own package.json");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.UninstallPackageAsync("sax");
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package uninstalled.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package uninstalled.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules after package installed.");
            }
        }

        [TestMethod, Priority(0), TestCategory("AppVeyorIgnore")]
        public void AddPackageNoPackageJsonThenUninstall()
        {
            using (var manager = new TemporaryFileManager())
            {
                var rootDir = FilesystemPackageJsonTestHelpers.CreateRootPackage(manager, PkgSimple);
                File.Delete(Path.Combine(rootDir, "package.json"));
                var controller = NpmControllerFactory.Create(rootDir, string.Empty);
                controller.Refresh();
                var rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package with no dependencies should not be null.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules before package install.");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.InstallPackageByVersionAsync("sax", "*", DependencyType.Standard, true);
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package installed.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package installed.");
                Assert.AreEqual(1, rootPackage.Modules.Count, "Should be one module after package installed.");

                var module = controller.RootPackage.Modules["sax"];
                Assert.IsNotNull(module, "Installed package should not be null.");
                Assert.AreEqual("sax", module.Name, "Module name mismatch.");
                Assert.IsNotNull(module.PackageJson, "Module package.json should not be null.");
                Assert.IsFalse(
                    module.IsListedInParentPackageJson,
                    "Should be listed as a dependency in parent package.json.");
                Assert.IsFalse(module.IsMissing, "Should not be marked as missing.");
                Assert.IsFalse(module.IsDevDependency, "Should not be marked as dev dependency.");
                Assert.IsFalse(module.IsOptionalDependency, "Should not be marked as optional dependency.");
                Assert.IsFalse(module.IsBundledDependency, "Should not be marked as bundled dependency.");
                Assert.IsTrue(module.HasPackageJson, "Module should have its own package.json");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.UninstallPackageAsync("sax");
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package uninstalled.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package uninstalled.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules after package installed.");
            }
        }

        [TestMethod, Priority(0), TestCategory("AppVeyorIgnore")]
        public void AddPackageNoSavePackageJsonThenUninstall()
        {
            using (var manager = new TemporaryFileManager())
            {
                var rootDir = FilesystemPackageJsonTestHelpers.CreateRootPackage(manager, PkgSimple);
                var controller = NpmControllerFactory.Create(rootDir, string.Empty);
                controller.Refresh();
                var rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package with no dependencies should not be null.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules before package install.");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.InstallPackageByVersionAsync("sax", "*", DependencyType.Standard, false);
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package installed.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package installed.");
                Assert.AreEqual(1, rootPackage.Modules.Count, "Should be one module after package installed.");

                var module = controller.RootPackage.Modules["sax"];
                Assert.IsNotNull(module, "Installed package should not be null.");
                Assert.AreEqual("sax", module.Name, "Module name mismatch.");
                Assert.IsNotNull(module.PackageJson, "Module package.json should not be null.");
                Assert.IsFalse(
                    module.IsListedInParentPackageJson,
                    "Should not be listed as a dependency in parent package.json.");
                Assert.IsFalse(module.IsMissing, "Should not be marked as missing.");
                Assert.IsFalse(module.IsDevDependency, "Should not be marked as dev dependency.");
                Assert.IsFalse(module.IsOptionalDependency, "Should not be marked as optional dependency.");
                Assert.IsFalse(module.IsBundledDependency, "Should not be marked as bundled dependency.");
                Assert.IsTrue(module.HasPackageJson, "Module should have its own package.json");

                using (var commander = controller.CreateNpmCommander())
                {
                    Task<bool> task = commander.UninstallPackageAsync("sax");
                    task.Wait();
                }

                Assert.AreNotEqual(
                    rootPackage,
                    controller.RootPackage,
                    "Root package should be different after package uninstalled.");

                rootPackage = controller.RootPackage;
                Assert.IsNotNull(rootPackage, "Root package should not be null after package uninstalled.");
                Assert.AreEqual(0, rootPackage.Modules.Count, "Should be no modules after package installed.");
            }
        }
    }
}

