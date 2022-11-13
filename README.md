# MonoCache
<p align="center">
  <a>
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <img alt="License" src="https://img.shields.io/github/license/MeeXaSiK/MonoCache?logo=github">
  </a>
  <a>
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/MeeXaSiK/MonoCache?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/MeeXaSiK/MonoCache?logo=VirtualBox">
  </a>
  <a>
    <img alt="Downloads" src="https://img.shields.io/github/downloads/MeeXaSiK/MonoCache/total?color=brightgreen">
  </a>
  <a>
    <img alt="Last Release" src="https://img.shields.io/github/v/release/MeeXaSiK/MonoCache?include_prereleases&logo=Dropbox&color=yellow">
  </a>
</p>


> MonoCache is a Fast Update Optimization-Caching Framework for Unity by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode)

* The framework caches all Update methods in one thread, that gives **≈25% performance**

* Also framework gives more useful features (more details below in the documentation)

## Navigation

* [Main](#monocache-20)
  * [Installation](#installation)
  * [How to use](#how-to-use)
  * [Need to know](#warning)
* [MonoAllocation](#monoallocation)
* [NightSugar](#nightsugar)
* [GetInfo < TClass >](#getinfot)
* [Singleton](#singleton)

## Installation

1. Add files into yout Unity project
2. Add component `GlobalUpdate` on any `GameObject` in scene

   > otherwise it will be added automatically, but it is better to add `GlobalUpdate` manually
   
## How to use

```csharp
using NTC.Global.Cache;

public class Demo : MonoCache
{
    protected override void OnEnabled()
    {
        //Replaces base "OnEnable()" method
    }

    protected override void OnDisabled()
    {
        //Replaces base "OnDisable()" method
    }

    protected override void Run()
    {
        //Replaces base "Update()" method
    }

    protected override void FixedRun()
    {
        //Replaces base "FixedUpdate()" method
    }

    protected override void LateRun()
    {
        //Replaces base "LateUpdate()" method
    }
}
```

## Warning

If you implement `OnEnable()` or `OnDisable()` methods in subclass of `MonoCache`, then it won't work correctly or at all.

> But you shouldn't worry, as in this case an error will be displayed in the console

![Exception Screenshot](https://github.com/MeeXaSiK/MonoCache/blob/main/README%20Files/Exception_Screenshot.png)

## MonoAllocation

> ***You can get components cached (≈30% faster than regular methods)***

| Old | New | With Allocation |
| ------ | ------ | ------ |
| ```GetComponent<T>()``` | ```Get<T>()``` | ```GetCached<T>()``` |
| ```GetComponents<T>()``` | ```Gets<T>()``` | ```GetsCached<T>()``` |
| ```GetComponentInChildren<T>()``` | ```ChildrenGet<T>()``` | ```ChildrenGetCached<T>()``` |
| ```GetComponentsInChildren<T>()``` | ```ChildrenGets<T>()``` | ```ChildrenGetsCached<T>()``` |
| ```GetComponentInParent<T>()``` | ```ParentGet<T>()``` | ```ParentGetCached<T>()``` |
| ```GetComponentsInParent<T>()``` | ```ParentGets<T>()``` | ```ParentGetsCached<T>()``` |
| ```FindObjectOfType<T>()``` | ```Find<T>()``` | ```FindCached<T>()``` |
| ```FindObjectsOfType<T>()``` | ```Finds<T>()``` | ```FindsCached<T>()``` |

#### Old implementation:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        var health = GetComponent<UnitHealth>();
        var viewModel = GetComponentInChildren<UnitViewModel>();
    }
}
```

#### New implementation

> Also you can use `EnableAllocation()` or `DisableAllocation()`. Allocation enabled initially

```csharp
public class Player : MonoCache //or MonoAllocation
{
    private void Start()
    {
        var health = Get<UnitHealth>();
        var viewModel = ChildrenGet<UnitViewModel>();
    }
}
```

## NightSugar

> `using NTC.Global.System;` or `using static NTC.Global.System.NightSugar`;

| Method | Info |
| ------ | ------ |
| `IfNotNull()` | `GetComponent<T>().IfNotNull(o => Debug.Log($"{typeof(T).Name} not null"))` |
| `IfNull()` | `GetComponent<T>().IfNull(o => Debug.Log($"{typeof(T).Name} is null"))` |
| `Enable()` | Replaces `SetActive(true)` |
| `Disable()` | Replaces `SetActive(false)` |
| `EnableParent()` | Tries to enable parent gameObject |
| `DisableParent()` | Tries to disable parent gameObject |
| `TryGetParent()` | Tries to get parent transform |
| `TryGetChild()` | Tries to get first child transform |
| `GetNearby<T>()` | Tries to get component in parent and in children |

## `GetInfo<T>`

You can get component index or type by `static class GetInfo<T>`

> `using NTC.Global.System;`

```csharp
  var id = GetInfo<Player>.Index;
  var type = GetInfo<Player>.Type;
```

## Singleton

[Get more information](https://github.com/MeeXaSiK/NightSingleton/)

## More information
- [Game optimization on Unity. Caching update.](https://youtu.be/7Dvir9Bf8X4)
- [Game optimization in Unity. We increase FPS.](https://youtu.be/JcqU2zHBwFY)

## Subscribe to Night Train Code channel
[Night Train Code](https://www.youtube.com/c/NightTrainCode)
