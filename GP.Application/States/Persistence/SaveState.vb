Friend Class SaveState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(, Brown, Black)
            .WriteLine("Save To Slot:")
            Dim index = 1
            For Each slot In SaveSlots
                .Write($"{index}. {slot.Item1}", Gray)
                If File.Exists(slot.Item2) Then
                    .Write(" (will overwrite)", Red)
                End If
                .WriteLine()
                index += 1
            Next
            .WriteLine("0. Go Back", Gray)
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(GameMenu)
            Case 1 To 5
                Engine.World.Save(SaveSlots(choice - 1).Item2)
                With FrameBuffer
                    .BackgroundColor = Black
                    .WriteLine()
                    .ForegroundColor = LightGreen
                    .WriteLine($"Game Saved to {SaveSlots(choice - 1).Item1}!")
                End With
                GoToState(GameMenu)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
