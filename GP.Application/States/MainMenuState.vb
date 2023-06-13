Public Class MainMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(ApplicationStates.ConfirmQuit)
            Case 1
                ChangeToState(ApplicationStates.Embark)
            Case 2
                ChangeToState(ApplicationStates.Load)
            Case 3
                ChangeToState(ApplicationStates.Options)
            Case Else
                Return False
        End Select
        Return True
    End Function
    Public Overrides Sub Start()
        _frameBuffer.ForegroundColor = Gray
        _frameBuffer.BackgroundColor = Black
        _frameBuffer.WriteLine()
        _frameBuffer.WriteLine("Main Menu:")
        _frameBuffer.WriteLine("1. Embark!")
        _frameBuffer.WriteLine("2. Load...")
        _frameBuffer.WriteLine("3. Options...")
        _frameBuffer.WriteLine("0. Quit")
        _frameBuffer.WriteLine()
        _frameBuffer.Write("> ")
    End Sub
End Class
