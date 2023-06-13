Public Class MainMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                _stateMachine.CurrentStateIdentifier = ApplicationStates.ConfirmQuit
                Return True
            Case Else
                Return False
        End Select
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
