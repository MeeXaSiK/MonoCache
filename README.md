# 🚄 MonoCache
[![License](https://img.shields.io/github/license/meexasik/monocache?color=318CE7&style=flat-square)](LICENSE.md) [![Version](https://img.shields.io/github/package-json/v/MeeXaSiK/MonoCache?color=318CE7&style=flat-square)](package.json) [![Unity](https://img.shields.io/badge/Unity-2020.3+-2296F3.svg?color=318CE7&style=flat-square)](https://unity.com/)

**MonoCache** is a fast framework for caching Unity update methods by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode)

🚀 The framework caches Unity update methods, which improves performance

# 🌐 Navigation

* [Main](#-monocache)
  * [Installation](#-installation)
  * [How to use](#-how-to-use)

# ▶ Installation
## As a Unity module
Supports installation as a Unity module via a git link in the **PackageManager**
```
https://github.com/MeeXaSiK/MonoCache.git
```
or direct editing of `Packages/manifest.json`:
```
"com.nighttraincode.monocache": "https://github.com/MeeXaSiK/MonoCache.git",
```
## As source
You can also clone the code into your Unity project.

# 🔸 How to use

Just use these `MonoCache` methods instead of the basic ones.

```csharp
using NTC.MonoCache;

public class Demo : MonoCache
{
    protected override void OnEnabled()
    {
        // Replaces base "OnEnable()" method.
    }

    protected override void OnDisabled()
    {
        // Replaces base "OnDisable()" method.
    }

    protected override void Run()
    {
        // Replaces base "Update()" method.
    }

    protected override void FixedRun()
    {
        // Replaces base "FixedUpdate()" method.
    }

    protected override void LateRun()
    {
        // Replaces base "LateUpdate()" method.
    }
}
```
