// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Testing;
using Newtonsoft.Json.Linq;
using Templates.Test.Helpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Templates.Blazor.Test;

public class BlazorWasmTemplateTest : BlazorTemplateTest
{
    public BlazorWasmTemplateTest(ProjectFactoryFixture projectFactory)
        : base(projectFactory) { }

    public override string ProjectType { get; } = "blazorwasm";

    [Fact]
    public async Task BlazorWasmStandaloneTemplateCanCreateBuildPublish()
    {
        var project = await CreateBuildPublishAsync();

        // The service worker assets manifest isn't generated for non-PWA projects
        var publishDir = Path.Combine(project.TemplatePublishDir, "wwwroot");
        Assert.False(File.Exists(Path.Combine(publishDir, "service-worker-assets.js")), "Non-PWA templates should not produce service-worker-assets.js");
    }

    [Fact]
    public async Task BlazorWasmStandaloneTemplateNoHttpsCanCreateBuildPublish()
    {
        var project = await CreateBuildPublishAsync(args: new[] { ArgConstants.NoHttps });

        // The service worker assets manifest isn't generated for non-PWA projects
        var publishDir = Path.Combine(project.TemplatePublishDir, "wwwroot");
        Assert.False(File.Exists(Path.Combine(publishDir, "service-worker-assets.js")), "Non-PWA templates should not produce service-worker-assets.js");
    }

    [Fact]
    public Task BlazorWasmStandalonePwaTemplateCanCreateBuildPublish()
        => CreateBuildPublishAsync(args: new[] { ArgConstants.Pwa });

    [Fact]
    public Task BlazorWasmStandalonePwaTemplateNoHttpsCanCreateBuildPublish()
        => CreateBuildPublishAsync(args: new[] { ArgConstants.Pwa, ArgConstants.NoHttps });
}
