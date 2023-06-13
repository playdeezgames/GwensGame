Public Class ConfirmQuitState
    Inherits BaseState
    Private ReadOnly _quitAction As Action
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer, quitAction As Action)
        MyBase.New(stateMachine, frameBuffer)
        _quitAction = quitAction
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                _stateMachine.CurrentStateIdentifier = ApplicationStates.MainMenu
                Return True
            Case 1
                _quitAction()
                Return True
            Case Else
                Return False
        End Select
    End Function
    Public Overrides Sub Start()
        _frameBuffer.BackgroundColor = Black
        _frameBuffer.WriteLine()
        _frameBuffer.ForegroundColor = Red
        _frameBuffer.WriteLine("Are you sure you want to quit?")
        _frameBuffer.ForegroundColor = Gray
        _frameBuffer.WriteLine("1. Yes")
        _frameBuffer.WriteLine("0. No")
        _frameBuffer.WriteLine()
        _frameBuffer.Write("> ")
    End Sub
End Class
