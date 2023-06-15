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
                GoToState(GameStates.MainMenu)
            Case 1
                _quitAction()
            Case Else
                Return False
        End Select
        Return True
    End Function
    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(,, Black)
            .WriteLine("Are you sure you want to quit?", LightRed)
            .WriteLine("1. Yes", Gray)
            .WriteLine("0. No")
        End With
        ShowPrompt()
    End Sub
End Class
