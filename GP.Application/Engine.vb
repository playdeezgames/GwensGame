Public Class Engine
    Inherits BasePresenter
    Implements IEngine, IApplicationStateMachine
    Private ReadOnly _config As GPConfig
    Private ReadOnly _frameBuffer As IFrameBuffer
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
        _config = config
        _frameBuffer = frameBuffer
    End Sub
    Public Sub Update(commands As IEnumerable(Of String), ticks As Long) Implements IEngine.Update
        For Each command In commands
            If command.Length = 1 Then
                Select Case command
                    Case ChrW(8)
                        If _lineBuffer.Any Then
                            'TODO: fix wrapping line
                            _frameBuffer.CursorColumn -= 1
                            _frameBuffer.Write(" "c)
                            _frameBuffer.CursorColumn -= 1
                            _lineBuffer = If(_lineBuffer.Any, _lineBuffer.Substring(0, _lineBuffer.Length - 1), _lineBuffer)
                        End If
                    Case ChrW(13)
                        _frameBuffer.WriteLine("")
                        Execute()
                        _lineBuffer = String.Empty
                    Case Else
                        _frameBuffer.Write(command)
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
        AddState(MainMenu, New MainMenuState(Me, _frameBuffer))
        AddState(ConfirmQuit, New ConfirmQuitState(Me, _frameBuffer, AddressOf DoQuit))
        AddState(Options, New OptionsState(Me, _frameBuffer, AddressOf DoToggleFullscreen))
        AddState(ScreenSize, New ScreenSizeState(Me, _frameBuffer, AddressOf DoSetScreenSize))
        AddState(Volume, New VolumeState(Me, _frameBuffer, AddressOf DoSetVolume, AddressOf DoGetVolume))
        AddState(Embark, New EmbarkState(Me, _frameBuffer))
        AddState(Load, New LoadState(Me, _frameBuffer))
        AddState(Neutral, New NeutralState(Me, _frameBuffer))
        AddState(GameMenu, New GameMenuState(Me, _frameBuffer))
        AddState(ConfirmAbandon, New ConfirmAbandonState(Me, _frameBuffer))
        AddState(Save, New SaveState(Me, _frameBuffer))
        AddState(KeepGoing, New KeepGoingState(Me, _frameBuffer))
        AddState(Win, New WinState(Me, _frameBuffer))
        AddState(Dead, New DeadState(Me, _frameBuffer))
    End Sub

    Private Sub SplashScreen()
        With _frameBuffer
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
        _config.Volume = volume
        DoVolume(_config.Volume)
        DoSfx(PlayerHit)
        _config.Save()
    End Sub
    Private Function DoGetVolume() As Single
        Return _config.Volume
    End Function
    Private Sub DoSetScreenSize(scale As Integer)
        _config.Scale = scale
        DoResize(_config.Scale, _config.FullScreen)
        _config.Save()
    End Sub

    Private Sub DoToggleFullscreen()
        _config.FullScreen = Not _config.FullScreen
        DoResize(_config.Scale, _config.FullScreen)
        _config.Save()
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
End Class
