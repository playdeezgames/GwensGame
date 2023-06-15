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
        With FrameBuffer
            .WriteLine(, Brown, Black)
            .WriteLine("Options:")
            .WriteLine("1. Toggle Fullscreen", Gray)
            .WriteLine("2. Set Display Size...")
            .WriteLine("3. Set Volume...")
            .WriteLine("0. Go Back")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(ExitState)
            Case 1
                _toggleFullscreen()
                Run()
            Case 2
                GoToState(GameStates.ScreenSize)
            Case 3
                GoToState(GameStates.Volume)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
