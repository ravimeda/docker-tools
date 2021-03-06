// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.DotNet.ImageBuilder.Model;

namespace Microsoft.DotNet.ImageBuilder.ViewModel
{
    public class TagInfo
    {
        private string BuildContextPath { get; set; }
        public string FullyQualifiedName { get; private set; }
        public Tag Model { get; private set; }
        public string Name { get; private set; }

        private TagInfo()
        {
        }

        public static TagInfo Create(
            string name,
            Tag model,
            string repoName,
            VariableHelper variableHelper,
            string buildContextPath = null)
        {
            TagInfo tagInfo = new TagInfo();
            tagInfo.Model = model;
            tagInfo.BuildContextPath = buildContextPath;
            tagInfo.Name = variableHelper.SubstituteValues(name, tagInfo.GetSubstituteValue);
            tagInfo.FullyQualifiedName = $"{repoName}:{tagInfo.Name}";

            return tagInfo;
        }

        public string GetSubstituteValue(string variableName)
        {
            string variableValue = null;
            if (variableName == "DockerfileGitCommitSha" && this.BuildContextPath != null)
            {
                variableValue = GitHelper.GetCommitSha(this.BuildContextPath);
            }
            return variableValue;
        }
    }
}
