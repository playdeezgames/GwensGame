Public Class MainMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
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
                _stateMachine.CurrentStateIdentifier = ApplicationStates.ConfirmQuit
            Case 1
                Me.ShowError(errorText)
            Case 2
                Me.ShowError(errorText)
            Case 3
                Me.ShowError(errorText)
            Case Else
                Me.ShowError(errorText)
        End Select
    End Sub
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
