// For more information see https://aka.ms/fsharp-console-apps

open System
open System.IO

let ExtractNumber (input : string) : string =
    $"{input[0]}{input[input.Length - 1]}"

let ExtractNumberFromString (input : string) : int =
    input
    |> String.filter Char.IsDigit
    |> ExtractNumber
    |> int    
    
let Day1 (input : string) : int =
    input.Split "\n"
    |> Array.map ExtractNumberFromString
    |> Array.sum
    
let input = File.ReadAllText("input.txt")
let output = Day1 input
printfn $"{output}"