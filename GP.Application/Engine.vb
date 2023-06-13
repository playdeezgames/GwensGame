Public Class Engine
    Inherits BasePresenter
    Implements IEngine, IApplicationStateMachine
    Private ReadOnly _config As GPConfig
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _lineBuffer As String = String.Empty
    Private ReadOnly _applicationStates As New Dictionary(Of String, IApplicationState)
    Private _currentApplicationStateIdentifier As String
    Public Property CurrentStateIdentifier As String Implements IApplicationStateMachine.CurrentStateIdentifier
        Get
            Return _currentApplicationStateIdentifier
        End Get
        Set(value As String)
            _currentApplicationStateIdentifier = value
            CurrentState.Start()
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
        _frameBuffer.Clear()
        _frameBuffer.ForegroundColor = LightRed
        _frameBuffer.WriteLine("                          ___                 _")
        _frameBuffer.WriteLine("                         / __|_ __ _____ _ _ ( )___")
        _frameBuffer.WriteLine("                        | (_ \ V  V / -_) ' \|/(_-<")
        _frameBuffer.WriteLine("              ___        \___|\_/\_/\___|_||_| /__/_   _")
        _frameBuffer.WriteLine("             | _ \___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _")
        _frameBuffer.WriteLine("             |  _/ -_) '_/ -_) _` | '_| | ' \/ _` |  _| / _ \ ' \")
        _frameBuffer.WriteLine("             |_| \___|_| \___\__, |_| |_|_||_\__,_|\__|_\___/_||_|")
        _frameBuffer.WriteLine("                             |___/")
        _frameBuffer.ForegroundColor = White
        _frameBuffer.WriteLine("                              Gwen's Peregrination")
        _frameBuffer.ForegroundColor = Gray
        _frameBuffer.WriteLine("                        A Production of TheGrumpyGameDev")
        AddState(MainMenu, New MainMenuState(Me, _frameBuffer))
        AddState(ConfirmQuit, New ConfirmQuitState(Me, _frameBuffer, AddressOf DoQuit))
        AddState(Options, New OptionsState(Me, _frameBuffer, AddressOf DoToggleFullscreen))
        AddState(ScreenSize, New ScreenSizeState(Me, _frameBuffer, AddressOf DoSetScreenSize))
        AddState(Volume, New VolumeState(Me, _frameBuffer, AddressOf DoSetVolume, AddressOf DoGetVolume))
        CurrentStateIdentifier = MainMenu
    End Sub
    Private Sub DoSetVolume(volume As Single)
        _config.Volume = volume
        DoVolume(_config.Volume)
        DoSfx(PlayerHit)
    End Sub
    Private Function DoGetVolume() As Single
        Return _config.Volume
    End Function
    Private Sub DoSetScreenSize(scale As Integer)
        _config.Scale = scale
        DoResize(_config.Scale, _config.FullScreen)
    End Sub

    Private Sub DoToggleFullscreen()
        _config.FullScreen = Not _config.FullScreen
        DoResize(_config.Scale, _config.FullScreen)
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
