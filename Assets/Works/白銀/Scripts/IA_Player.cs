//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Works/白銀/IA_Player.inputactions
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

public partial class @IA_Player: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @IA_Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""IA_Player"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""cd014029-ca9b-4556-bbaf-6e0fb829d76b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7a0cbea7-eeb8-4c25-9b4e-e26e611876a8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c67d7071-fbfa-4db6-a626-3b3912e89f8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""a7d3673d-f47c-40a8-94cc-a19ae73ed623"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SlideL"",
                    ""type"": ""Button"",
                    ""id"": ""195d42fa-2ee6-41c0-999d-4528d08c2c75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SlideR"",
                    ""type"": ""Button"",
                    ""id"": ""b9c650c6-5e38-4ffb-b564-a1320b75b90d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""680f2fb8-00fc-4ff2-9a4d-59fde7687c8b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""24b89fc6-d4bc-4875-b60f-5a3ccfe04e6f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""93d5aa0b-adbc-4453-80cb-01ebf7cccfbb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e19392ab-4015-4b0e-b76d-ef220818ab32"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fd8e4adb-b974-4458-aade-1e56894431c1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6d24dab5-5e72-475e-a916-cf1991845293"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7955b34d-7ebd-4923-a3d8-c13909e2d552"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8526c2b3-508e-4120-8468-a1389fac6b73"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2342ec1f-5607-45c8-a71d-78205011dd4f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d72b6fb-c44a-4313-bb04-3d7a2c643a11"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c376fd32-1e2a-43bd-98c2-5b3f97904aeb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0062881-3791-4020-ac03-2c1a5eb09e4c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4691d37f-b25b-4e7f-9c42-74b7341af956"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlideL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd0951b3-dd59-40da-b69e-9c2821c5b36c"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlideR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Title"",
            ""id"": ""86fdf6b1-b545-49f8-9ddb-29a6816da82c"",
            ""actions"": [
                {
                    ""name"": ""ToGameScene"",
                    ""type"": ""Button"",
                    ""id"": ""5649a849-3882-43c1-ad9e-d8c27fdb6dd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4473708a-496b-4463-82cc-fd58f8b00007"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToGameScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7be3778d-a8aa-4050-b69c-645f7968e832"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToGameScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Slide = m_Player.FindAction("Slide", throwIfNotFound: true);
        m_Player_SlideL = m_Player.FindAction("SlideL", throwIfNotFound: true);
        m_Player_SlideR = m_Player.FindAction("SlideR", throwIfNotFound: true);
        // Title
        m_Title = asset.FindActionMap("Title", throwIfNotFound: true);
        m_Title_ToGameScene = m_Title.FindAction("ToGameScene", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Slide;
    private readonly InputAction m_Player_SlideL;
    private readonly InputAction m_Player_SlideR;
    public struct PlayerActions
    {
        private @IA_Player m_Wrapper;
        public PlayerActions(@IA_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Slide => m_Wrapper.m_Player_Slide;
        public InputAction @SlideL => m_Wrapper.m_Player_SlideL;
        public InputAction @SlideR => m_Wrapper.m_Player_SlideR;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Slide.started += instance.OnSlide;
            @Slide.performed += instance.OnSlide;
            @Slide.canceled += instance.OnSlide;
            @SlideL.started += instance.OnSlideL;
            @SlideL.performed += instance.OnSlideL;
            @SlideL.canceled += instance.OnSlideL;
            @SlideR.started += instance.OnSlideR;
            @SlideR.performed += instance.OnSlideR;
            @SlideR.canceled += instance.OnSlideR;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Slide.started -= instance.OnSlide;
            @Slide.performed -= instance.OnSlide;
            @Slide.canceled -= instance.OnSlide;
            @SlideL.started -= instance.OnSlideL;
            @SlideL.performed -= instance.OnSlideL;
            @SlideL.canceled -= instance.OnSlideL;
            @SlideR.started -= instance.OnSlideR;
            @SlideR.performed -= instance.OnSlideR;
            @SlideR.canceled -= instance.OnSlideR;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Title
    private readonly InputActionMap m_Title;
    private List<ITitleActions> m_TitleActionsCallbackInterfaces = new List<ITitleActions>();
    private readonly InputAction m_Title_ToGameScene;
    public struct TitleActions
    {
        private @IA_Player m_Wrapper;
        public TitleActions(@IA_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToGameScene => m_Wrapper.m_Title_ToGameScene;
        public InputActionMap Get() { return m_Wrapper.m_Title; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TitleActions set) { return set.Get(); }
        public void AddCallbacks(ITitleActions instance)
        {
            if (instance == null || m_Wrapper.m_TitleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TitleActionsCallbackInterfaces.Add(instance);
            @ToGameScene.started += instance.OnToGameScene;
            @ToGameScene.performed += instance.OnToGameScene;
            @ToGameScene.canceled += instance.OnToGameScene;
        }

        private void UnregisterCallbacks(ITitleActions instance)
        {
            @ToGameScene.started -= instance.OnToGameScene;
            @ToGameScene.performed -= instance.OnToGameScene;
            @ToGameScene.canceled -= instance.OnToGameScene;
        }

        public void RemoveCallbacks(ITitleActions instance)
        {
            if (m_Wrapper.m_TitleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITitleActions instance)
        {
            foreach (var item in m_Wrapper.m_TitleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TitleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TitleActions @Title => new TitleActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnSlideL(InputAction.CallbackContext context);
        void OnSlideR(InputAction.CallbackContext context);
    }
    public interface ITitleActions
    {
        void OnToGameScene(InputAction.CallbackContext context);
    }
}
