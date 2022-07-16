//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Gibson/New Input System.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ShootNew : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShootNew()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Input System"",
    ""maps"": [
        {
            ""name"": ""Shoot"",
            ""id"": ""ca31c69f-f173-4747-95da-7ba7487366df"",
            ""actions"": [
                {
                    ""name"": ""shoot"",
                    ""type"": ""Button"",
                    ""id"": ""cc8f6ca9-d72e-41f2-b935-ebc464b64c69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""aab170f4-70a1-4a05-b1ca-ede564c067ed"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Shoot
        m_Shoot = asset.FindActionMap("Shoot", throwIfNotFound: true);
        m_Shoot_shoot = m_Shoot.FindAction("shoot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Shoot
    private readonly InputActionMap m_Shoot;
    private IShootActions m_ShootActionsCallbackInterface;
    private readonly InputAction m_Shoot_shoot;
    public struct ShootActions
    {
        private @ShootNew m_Wrapper;
        public ShootActions(@ShootNew wrapper) { m_Wrapper = wrapper; }
        public InputAction @shoot => m_Wrapper.m_Shoot_shoot;
        public InputActionMap Get() { return m_Wrapper.m_Shoot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShootActions set) { return set.Get(); }
        public void SetCallbacks(IShootActions instance)
        {
            if (m_Wrapper.m_ShootActionsCallbackInterface != null)
            {
                @shoot.started -= m_Wrapper.m_ShootActionsCallbackInterface.OnShoot;
                @shoot.performed -= m_Wrapper.m_ShootActionsCallbackInterface.OnShoot;
                @shoot.canceled -= m_Wrapper.m_ShootActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_ShootActionsCallbackInterface = instance;
            if (instance != null)
            {
                @shoot.started += instance.OnShoot;
                @shoot.performed += instance.OnShoot;
                @shoot.canceled += instance.OnShoot;
            }
        }
    }
    public ShootActions @Shoot => new ShootActions(this);
    public interface IShootActions
    {
        void OnShoot(InputAction.CallbackContext context);
    }
}
