# MonoCache

> MonoCache is a Fast Update Optimization-Caching Framework for Unity by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode)

* The framework caches all Update methods in one thread, that gives **≈25% performance**
* Also framework gives more useful features (more details below in the documentation)

## Navigation

* [Main](#monocache)
  * [Installation](#installation)
  * [How to use](#how-to-use)
  * [Need to know](#warning)
* [MonoShortCuts](#monoshortcuts)
* [NightSugar](#nightsugar)
* [GetInfo < TClass >](#getinfot)
* [Singleton](#singleton)

## Installation

1. Install `MonoCache` in your Unity project
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

## MonoShortCuts

| Old | New | 
| ------ | ------ |
| ```GetComponent<T>()``` | ```Get<T>()``` |
| ```GetComponents<T>()``` | ```Gets<T>()``` |
| ```GetComponentInChildren<T>()``` | ```ChildrenGet<T>()``` |
| ```GetComponentsInChildren<T>()``` | ```ChildrenGets<T>()``` |
| ```GetComponentInParent<T>()``` | ```ParentGet<T>()``` |
| ```GetComponentsInParent<T>()``` | ```ParentGets<T>()``` |
| ```FindObjectOfType<T>()``` | ```Find<T>()``` |
| ```FindObjectsOfType<T>()``` | ```Finds<T>()``` |

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

```csharp
public class Player : MonoCache //or MonoShortCuts
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
| `Unparent()` | Sets null parent for transform |
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
