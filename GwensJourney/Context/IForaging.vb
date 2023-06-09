Public Interface IForaging
    Sub GenerateDifficulty()
    Sub GenerateAbundance()
    Sub Reset()
    Function DifficultyName() As String
    Function CanForage() As Boolean
    ReadOnly Property Difficulty As Integer
    Property Abundance As Integer
End Interface
