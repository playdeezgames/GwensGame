Public Class ConfirmQuitState
    Inherits BaseState
    Private ReadOnly _quitAction As Action
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer, quitAction As Action)
        MyBase.New(stateMachine, frameBuffer)
        _quitAction = quitAction
    End Sub
    Public Overrides Sub Handle(tokens As IEnumerable(Of String))
        Const errorText = "Please enter a valid number!"
        If tokens.Count > 1 Then
            Me.ShowError(errorText)
            Return
        End If
        Dim choice As Integer
        If Not Int32.TryParse(tokens.First, choice) Then
            Me.ShowError(errorText)
            Return
        End If
        Select Case choice
            Case 0
                _stateMachine.CurrentStateIdentifier = ApplicationStates.MainMenu
            Case 1
                _quitAction()
            Case Else
                Me.ShowError(errorText)
        End Select
    End Sub
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
