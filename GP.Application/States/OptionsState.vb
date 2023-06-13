Friend Class OptionsState
    Inherits BaseState
    Implements IApplicationState
    Private ReadOnly _toggleFullscreen As Action
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer, toggleFullscreen As Action)
        MyBase.New(stateMachine, frameBuffer)
        _toggleFullscreen = toggleFullscreen
    End Sub
    Public Overrides Sub Start()
        _frameBuffer.BackgroundColor = Black
        _frameBuffer.WriteLine()
        _frameBuffer.ForegroundColor = Brown
        _frameBuffer.WriteLine("Options:")
        _frameBuffer.ForegroundColor = Gray
        _frameBuffer.WriteLine("1. Toggle Fullscreen")
        _frameBuffer.WriteLine("2. Set Display Size...")
        _frameBuffer.WriteLine("3. Set Volume...")
        _frameBuffer.WriteLine("0. Go Back")
        _frameBuffer.WriteLine()
        _frameBuffer.Write("> ")
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(ApplicationStates.MainMenu)
            Case 1
                _toggleFullscreen()
                Start()
            Case 2
                ChangeToState(ApplicationStates.ScreenSize)
            Case 3
                ChangeToState(ApplicationStates.Volume)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
