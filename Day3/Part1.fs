module Day3Part1

open System
open System.IO

let CalculateNextXAndY (x : int) (y : int) (max : int) =
    match x + 1 with
    | i when i = max -> (0, y + 1)
    | _ -> (x + 1, y)

let rec ParseNumber (row : string) (index : int) (current : string) : string =
    if index >= row.Length || not (Char.IsNumber (row.Chars(index))) then
        current
    else
        ParseNumber row (index + 1) $"{current}{row.Chars(index)}"

let rec ExtractNumber (row : string) (index : int) : int =
    if index < 0 || not (Char.IsNumber (row.Chars(index))) then
        ParseNumber row (index + 1) "" |> int
    else
        ExtractNumber row (index - 1)
        
let IsSymbol character =
    not (Char.IsNumber character) && character <> '.'         

let rec GetNeighbourValue (input : string list) (x : int) (y : int) : int =
    if x < 0 || y < 0 || x >= input[0].Length || y >= input.Length || not (IsSymbol (input[y].Chars(x))) then
        0
    else
        1

let CountNeighbours (input : string list) (x : int) (y : int) : int =
    (GetNeighbourValue input x (y - 1)) + (GetNeighbourValue input x y) + (GetNeighbourValue input x (y + 1))

let GetAdjacentSymbolCount (input : string list) (startX : int) (startY : int) =
    let rec Loop (input : string list) (x : int) (y : int) (count : int) =   
        if x = input[y].Length then
            count
        else if x >= startX && not (Char.IsNumber (input[y].Chars(x))) then
            count + (CountNeighbours input x y)
        else
            let adjacentSymbolCount = CountNeighbours input x y
            Loop input (x + 1) y (count + adjacentSymbolCount)
    Loop input (startX - 1) startY 0
    
let ProcessNumber (input : string list) (x : int) (y : int) : int =
    let adjacentSymbolCount = GetAdjacentSymbolCount input x y
    if adjacentSymbolCount > 0 then
        ExtractNumber input[y] x
    else
        0

let ProcessCell (input : string list) (x : int) (y : int) : int =
    if Char.IsNumber (input[y].Chars(x)) && (x - 1 < 0 || not (Char.IsNumber (input[y].Chars(x - 1)))) then
        ProcessNumber input x y
    else
        0
        
let Day3Part1 (input : string list) =
    let rec Loop (input : string list) (x : int) (y : int) (count : int) = 
        if y = input.Length then
            count
        else
            let nextX, nextY = CalculateNextXAndY x y input[0].Length
            let processedCellCount = ProcessCell input x y
            Loop input nextX nextY (count + processedCellCount)
    Loop input 0 0 0

File.ReadAllLines "input.txt"
|> Array.toList
|> Day3Part1
|> printfn "%i"
    