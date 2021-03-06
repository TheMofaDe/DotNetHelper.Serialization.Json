    public class BuildMetadata
    {
        public string GitBaseVersion { get; set; }
        public string GitSemVersion { get; set; }
        public string GitCommits { get; set; }
        public string GitBaseVersionMajor { get; set; }
        public string GitBaseVersionMinor { get; set; } 
        public string GitBaseVersionPatch { get; set; }
        public string GitSemVerMajor { get; set; } 
        public string GitSemVerMinor { get; set; } 
        public string GitSemVerPatch { get; set; } 
        public string AssemblyVersion { get; set; }
        public string FileVersion { get; set; }
        public string InformationalVersion { get; set; }
        public string PackageVersion { get; set; }
        public string Version { get; set; }
        public string VersionPrefix { get; set; }
        public string VersionSuffix { get; set; }
        public string SemVerDashLabel  { get; set; }
        public string GitTag  { get; set; }
        public string GitBaseTag  { get; set; }
        public string GitIsDirty  { get; set; }

        public string PackageProjectUrl  { get; set; }
        public string RepositoryUrl  { get; set; }
        public string PackageReleaseNotes  { get; set; }
        public string RepositoryCommit  { get; set; }
        public string RepositoryBranch  { get; set; }

        public DateTime GitCommitDate { get;set; }

        public string SemVersion => $"{GitSemVerMajor}.{GitSemVerMinor}.{GitSemVerPatch}";
        public string BaseVesion => $"{GitBaseVersionMajor}.{GitBaseVersionMinor}.{GitBaseVersionPatch}";
    }