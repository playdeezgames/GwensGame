Friend Class SaveState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .ForegroundColor = Brown
            .WriteLine()
            .WriteLine("Save To Slot:")
            .ForegroundColor = Gray
            Dim index = 1
            For Each slot In SaveSlots
                .Write($"{index}. {slot.Item1}")
                If File.Exists(slot.Item2) Then
                    .ForegroundColor = Red
                    .Write(" (will overwrite)")
                    .ForegroundColor = Gray
                End If
                .WriteLine()
                index += 1
            Next
            .WriteLine("0. Go Back")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(GameMenu)
            Case 1 To 5
                Engine.World.Save(SaveSlots(choice - 1).Item2)
                With _frameBuffer
                    .BackgroundColor = Black
                    .WriteLine()
                    .ForegroundColor = LightGreen
                    .WriteLine($"Game Saved to {SaveSlots(choice - 1).Item1}!")
                End With
                ChangeToState(GameMenu)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
