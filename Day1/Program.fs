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

let ReplaceWordWithDigit(input : string) : string =
    input
        .Replace("one", "on1e")
        .Replace("two", "tw2o")
        .Replace("three", "thre3e")
        .Replace("four", "fou4r")
        .Replace("five", "fiv5e")
        .Replace("six", "si6x")
        .Replace("seven", "seve7n")
        .Replace("eight", "eigh8t")
        .Replace("nine", "nin9e")
           
let Day1 (input : string) : int =
    input.Split "\n"
    |> Array.map ReplaceWordWithDigit
    |> Array.map ExtractNumberFromString
    |> Array.sum
    
let input = File.ReadAllText("input.txt")
let output = Day1 input
printfn $"{output}"