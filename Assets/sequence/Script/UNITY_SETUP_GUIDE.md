# Fish Hook Collision - Unity Inspector Setup Guide

## Problem
Fish are not attaching to the hook when they collide.

## Solution Checklist

Follow these steps **exactly** in the Unity Editor to fix the collision detection:

---

## 1. Fish Prefab Setup

### Step 1: Select the Fish Prefab
1. In Unity, navigate to your **Project** window
2. Find the fish prefab (referenced as `fishSpritePrefab` in FishController)
3. Double-click to open it in Prefab mode, OR select it to edit in Inspector

### Step 2: Add/Verify Collider2D
1. In the Inspector, check if there's a **Collider2D** component
   - If missing: Click **Add Component** → **Physics 2D** → **Circle Collider 2D** (or Box Collider 2D)
2. **CRITICAL**: Check the **"Is Trigger"** checkbox ✅
3. Adjust the collider size to match the fish sprite

### Step 3: Add/Verify Rigidbody2D
1. In the Inspector, check if there's a **Rigidbody2D** component
   - If missing: Click **Add Component** → **Physics 2D** → **Rigidbody 2D**
2. Set **Body Type** to **Kinematic**
3. Set **Gravity Scale** to **0**

### Step 4: Add/Verify FishTrigger Script
1. In the Inspector, check if **FishTrigger** script is attached
   - If missing: Click **Add Component** → Search "FishTrigger" → Add it
2. The script should show with an **"Enable Debug Logs"** checkbox (enabled by default)

### Step 5: Apply Changes
1. If editing a prefab instance, click **Overrides** → **Apply All**
2. If in Prefab mode, just save and exit

---

## 2. Hook GameObject Setup

### Step 1: Find the Hook in Hierarchy
1. In Unity, go to the **Hierarchy** window
2. Find the GameObject that represents the hook
   - It's likely the object referenced in PlayerController as the main player object
   - Or search for objects with "hook" in the name

### Step 2: Set the Tag
1. Select the hook GameObject
2. At the top of the Inspector, find the **Tag** dropdown
3. If "Hook" tag doesn't exist:
   - Click **Tag** dropdown → **Add Tag...**
   - Click the **+** button
   - Type "Hook" (case-sensitive!)
   - Go back to your hook GameObject
4. Set the **Tag** to **"Hook"** ✅

### Step 3: Add/Verify Collider2D
1. In the Inspector, check if there's a **Collider2D** component
   - If missing: Click **Add Component** → **Physics 2D** → **Circle Collider 2D**
2. **CRITICAL**: Check the **"Is Trigger"** checkbox ✅
3. Adjust the collider size (make it slightly larger than the hook sprite for easier catching)

### Step 4: Verify Rigidbody2D
1. The hook should already have a **Rigidbody2D** (from PlayerController)
2. Verify it exists in the Inspector

---

## 3. Testing & Debugging

### Step 1: Run the Game
1. Press **Play** in Unity Editor
2. Watch the **Console** window (Window → General → Console)

### Step 2: Check Debug Messages
When a fish collides with the hook, you should see:
```
[FishTrigger] Collision detected with: Hook (Tag: Hook)
[FishTrigger] Hook detected! Attempting to hook fish: Fish(Clone)
[FishTrigger] Fish Fish(Clone) successfully hooked!
```

### Step 3: Troubleshooting

**If you see NO messages at all:**
- ❌ Colliders are not colliding
- Check that both fish and hook have Collider2D components
- Check that at least one has "Is Trigger" enabled
- Check that fish has Rigidbody2D

**If you see "Collision detected" but wrong tag:**
```
[FishTrigger] Collision with non-hook object. Expected tag 'Hook', got 'Untagged'
```
- ❌ Hook GameObject is not tagged as "Hook"
- Go back to Step 2.2 and set the tag correctly

**If you see "FishController.Instance is NULL":**
```
[FishTrigger] FishController.Instance is NULL! Cannot hook fish.
```
- ❌ FishController is not in the scene or Awake() hasn't run
- Make sure FishController script is attached to a GameObject in the scene

**On game start, check for validation warnings:**
```
[FishTrigger] Fish(Clone) is missing a Collider2D component!
[FishTrigger] Fish(Clone)'s Collider2D 'Is Trigger' is NOT enabled!
```
- ❌ Follow the warnings and fix the setup

---

## 4. Quick Reference Checklist

### Fish Prefab ✅
- [ ] Has Collider2D (Circle or Box)
- [ ] Collider2D "Is Trigger" is checked
- [ ] Has Rigidbody2D
- [ ] Rigidbody2D Body Type = Kinematic
- [ ] Has FishTrigger script attached

### Hook GameObject ✅
- [ ] Tag is set to "Hook"
- [ ] Has Collider2D
- [ ] Collider2D "Is Trigger" is checked
- [ ] Has Rigidbody2D (from PlayerController)

---

## 5. Still Not Working?

If you've followed all steps and it's still not working:

1. **Enable Debug Logs**: Make sure "Enable Debug Logs" is checked on FishTrigger script
2. **Check Console**: Look for error messages or warnings
3. **Check Layers**: In Edit → Project Settings → Physics 2D, ensure the layers can collide
4. **Verify in Play Mode**: Select the fish and hook during play mode and verify components are active

---

## Additional Notes

- The debug logging can be disabled later by unchecking "Enable Debug Logs" on the FishTrigger component
- For better gameplay, you can increase the hook's collider radius to make catching easier
- Make sure the fish prefab changes are applied to ALL instances
