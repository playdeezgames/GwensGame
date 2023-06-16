Public Class Engine
    Inherits BasePresenter
    Implements IEngine, IApplicationStateMachine
    Private ReadOnly Config As GPConfig
    Private ReadOnly FrameBuffer As IFrameBuffer
    Private _lineBuffer As String = String.Empty
    Private ReadOnly _applicationStates As New Dictionary(Of String, IApplicationState)
    Private _currentApplicationStateIdentifier As String
    Friend Shared Property World As IWorld
    Public Property CurrentStateIdentifier As String Implements IApplicationStateMachine.CurrentStateIdentifier
        Get
            Return _currentApplicationStateIdentifier
        End Get
        Set(value As String)
            _currentApplicationStateIdentifier = value
            CurrentState.Run()
        End Set
    End Property

    Public ReadOnly Property CurrentState As IApplicationState Implements IApplicationStateMachine.CurrentState
        Get
            Return GetState(_currentApplicationStateIdentifier)
        End Get
    End Property

    Sub New(config As GPConfig, frameBuffer As IFrameBuffer)
        Me.Config = config
        Me.FrameBuffer = frameBuffer
    End Sub
    Public Sub Update(commands As IEnumerable(Of String), ticks As Long) Implements IEngine.Update
        For Each command In commands
            If command.Length = 1 Then
                Select Case command
                    Case ChrW(8)
                        If _lineBuffer.Any Then
                            'TODO: fix wrapping line
                            FrameBuffer.CursorColumn -= 1
                            FrameBuffer.Write(" "c)
                            FrameBuffer.CursorColumn -= 1
                            _lineBuffer = If(_lineBuffer.Any, _lineBuffer.Substring(0, _lineBuffer.Length - 1), _lineBuffer)
                        End If
                    Case ChrW(13)
                        FrameBuffer.WriteLine("")
                        Execute()
                        _lineBuffer = String.Empty
                    Case Else
                        FrameBuffer.Write(command)
                        _lineBuffer &= command
                End Select
            End If
        Next
    End Sub

    Public Sub Initialize() Implements IEngine.Initialize
        SplashScreen()
        WireUpStates()
        CurrentStateIdentifier = MainMenu
    End Sub

    Private Sub WireUpStates()
        AddState(MainMenu, New MainMenuState(Me, FrameBuffer))
        AddState(ConfirmQuit, New ConfirmQuitState(Me, FrameBuffer, AddressOf DoQuit))
        AddState(Options, New OptionsState(Me, FrameBuffer, AddressOf DoToggleFullscreen))
        AddState(ScreenSize, New ScreenSizeState(Me, FrameBuffer, AddressOf DoSetScreenSize))
        AddState(Volume, New VolumeState(Me, FrameBuffer, AddressOf DoSetVolume, AddressOf DoGetVolume))
        AddState(Embark, New EmbarkState(Me, FrameBuffer))
        AddState(Load, New LoadState(Me, FrameBuffer))
        AddState(Neutral, New NeutralState(Me, FrameBuffer))
        AddState(GameMenu, New GameMenuState(Me, FrameBuffer))
        AddState(ConfirmAbandon, New ConfirmAbandonState(Me, FrameBuffer))
        AddState(Save, New SaveState(Me, FrameBuffer))
        AddState(KeepGoing, New KeepGoingState(Me, FrameBuffer))
        AddState(Win, New WinState(Me, FrameBuffer))
        AddState(Dead, New DeadState(Me, FrameBuffer))
        AddState(ChangePace, New ChangePaceState(Me, FrameBuffer))
        AddState(EatSnax, New EatSnaxState(Me, FrameBuffer))
        AddState(Forage, New ForageState(Me, FrameBuffer))
        AddState(TakeShortcut, New TakeShortcutState(Me, FrameBuffer))
    End Sub

    Private Sub SplashScreen()
        With FrameBuffer
            .Clear()
            .ForegroundColor = LightRed
            .WriteLine("                          ___                 _")
            .WriteLine("                         / __|_ __ _____ _ _ ( )___")
            .WriteLine("                        | (_ \ V  V / -_) ' \|/(_-<")
            .WriteLine("              ___        \___|\_/\_/\___|_||_| /__/_   _")
            .WriteLine("             | _ \___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _")
            .WriteLine("             |  _/ -_) '_/ -_) _` | '_| | ' \/ _` |  _| / _ \ ' \")
            .WriteLine("             |_| \___|_| \___\__, |_| |_|_||_\__,_|\__|_\___/_||_|")
            .WriteLine("                             |___/")
            .ForegroundColor = White
            .WriteLine("                              Gwen's Peregrination")
            .ForegroundColor = DarkGray
            .WriteLine("                        A Production of TheGrumpyGameDev")
        End With
    End Sub

    Private Sub DoSetVolume(volume As Single)
        Config.Volume = volume
        DoVolume(Config.Volume)
        DoSfx(PlayerHit)
        Config.Save()
    End Sub
    Private Function DoGetVolume() As Single
        Return Config.Volume
    End Function
    Private Sub DoSetScreenSize(scale As Integer)
        Config.Scale = scale
        DoResize(Config.Scale, Config.FullScreen)
        Config.Save()
    End Sub

    Private Sub DoToggleFullscreen()
        Config.FullScreen = Not Config.FullScreen
        DoResize(Config.Scale, Config.FullScreen)
        Config.Save()
    End Sub

    Private Sub Execute()
        Dim tokens = _lineBuffer.ToLowerInvariant.Split(" "c)
        _lineBuffer = String.Empty
        CurrentState.Handle(tokens)
    End Sub

    Public Sub AddState(identifier As String, state As IApplicationState) Implements IApplicationStateMachine.AddState
        _applicationStates(identifier) = state
    End Sub

    Public Function GetState(identifier As String) As IApplicationState Implements IApplicationStateMachine.GetState
        Return If(_applicationStates.ContainsKey(identifier), _applicationStates(identifier), Nothing)
    End Function

    Public Shared Sub PlaySfx(sfx As String)
        RaiseEvent OnPlaySfx(sfx)
    End Sub
    Public Shared Event OnPlaySfx(sfx As String)
End Class
