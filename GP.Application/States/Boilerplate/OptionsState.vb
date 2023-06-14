Friend Class OptionsState
    Inherits BaseState
    Implements IApplicationState
    Private ReadOnly _toggleFullscreen As Action
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer, toggleFullscreen As Action)
        MyBase.New(stateMachine, frameBuffer)
        _toggleFullscreen = toggleFullscreen
    End Sub
    Friend Shared Property ExitState As String
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Options:")
            .ForegroundColor = Gray
            .WriteLine("1. Toggle Fullscreen")
            .WriteLine("2. Set Display Size...")
            .WriteLine("3. Set Volume...")
            .WriteLine("0. Go Back")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(ExitState)
            Case 1
                _toggleFullscreen()
                Run()
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
