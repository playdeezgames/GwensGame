Friend Module Constants
    Friend ReadOnly SaveSlots As IReadOnlyList(Of (String, String)) =
        New List(Of (String, String)) From
        {
            ("Slot 1", "slot1.json"),
            ("Slot 2", "slot2.json"),
            ("Slot 3", "slot3.json"),
            ("Slot 4", "slot4.json"),
            ("Slot 5", "slot5.json")
        }
    Private ReadOnly paceVisuals As IReadOnlyDictionary(Of Integer, (Integer, String)) =
        New Dictionary(Of Integer, (Integer, String)) From
        {
            {1, (Blue, "Very Slow")},
            {2, (Cyan, "Slow")},
            {3, (Green, "Medium")},
            {4, (Yellow, "Fast")},
            {5, (Red, "Very Fast")}
        }
    Friend Function GetPaceName(pace As Integer) As String
        Return paceVisuals(pace).Item2
    End Function
    Friend Function GetPaceColor(pace As Integer) As Integer
        Return paceVisuals(pace).Item1
    End Function
    Friend Function GetForageDifficultyName(difficulty As Integer) As String
        Select Case difficulty
            Case <= -30
                Return "Very Difficulty"
            Case -29 To -15
                Return "Difficult"
            Case -14 To 14
                Return "Moderate"
            Case 15 To 29
                Return "Easy"
            Case Else
                Return "Very Easy"
        End Select
    End Function
    Friend Function GetForageDifficultyColor(difficulty As Integer) As Integer
        Select Case difficulty
            Case <= -30
                Return Red
            Case -29 To -15
                Return Brown
            Case -14 To 14
                Return Yellow
            Case 15 To 29
                Return Green
            Case Else
                Return LightGreen
        End Select
    End Function
End Module
