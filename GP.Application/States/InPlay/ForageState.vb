Friend Class ForageState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        If Engine.World.Avatar.Forage() Then
            With FrameBuffer
                .WriteLine(,, Black)
                .WriteLine("You successfully forage!", Green)
            End With
        Else
            With FrameBuffer
                .WriteLine(,, Black)
                .WriteLine("You don't find anything!", Red)
            End With
        End If
        GoToState(Neutral)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
