Public MustInherit Class BaseState
    Implements IApplicationState
    Protected ReadOnly _stateMachine As IApplicationStateMachine
    Protected ReadOnly FrameBuffer As IFrameBuffer
    Protected Sub GoToState(state As String)
        _stateMachine.CurrentStateIdentifier = state
    End Sub
    Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        _stateMachine = stateMachine
        Me.FrameBuffer = frameBuffer
    End Sub
    Public Overridable Sub Handle(tokens As IEnumerable(Of String)) Implements IApplicationState.Handle
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
        If Not HandleChoice(choice) Then
            ShowError(errorText)
        End If
    End Sub
    Public MustOverride Sub Run() Implements IApplicationState.Run
    Protected MustOverride Function HandleChoice(choice As Integer) As Boolean
    Protected Sub ShowError(message As String)
        With FrameBuffer
            .WriteLine(, Red, Black)
            .WriteLine(message)
        End With
        Run()
    End Sub
    Protected Sub ShowPrompt()
        With FrameBuffer
            .WriteLine()
            .Write("> ", Cyan)
        End With
    End Sub
End Class
