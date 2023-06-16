Friend Class TakeShortcutState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        With FrameBuffer
            .WriteLine(,, Black)
            .WriteLine("You take the potential shortcut!", Gray)
            If avatar.Shortcut > 0 Then
                .WriteLine($"It made yer journey {avatar.Shortcut} mile(s) longer!", Red)
            Else
                .WriteLine($"It made yer journey {-avatar.Shortcut} mile(s) shorter!", Green)
            End If
        End With
        avatar.TakeShortcut()
        GoToState(Neutral)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
