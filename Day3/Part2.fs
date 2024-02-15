module Day3Part2

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
        
let IsGear character =
    character = '*'

let NeighbourIsGear (input : string list) (x : int) (y : int) =
    not (x < 0 || y < 0 || x >= input[0].Length || y >= input.Length || not (IsGear (input[y].Chars(x))))

let AddToGears (input : string list) (x : int) (y : int) =
    if NeighbourIsGear input x y then
       [(x, y)]
    else
       []

let GetNeighbourGears (input : string list) (x : int) (y : int) : (int * int) list =
    AddToGears input x (y - 1) @ AddToGears input x y @ AddToGears input x (y + 1)

let GetAdjacentGears (input : string list) (startX : int) (startY : int) : (int * int) list =
    let rec Loop (input : string list) (x : int) (y : int) (gears : (int * int) list) : (int * int) list =   
        if x = input[y].Length then
            gears
        elif x >= startX && not (Char.IsNumber (input[y].Chars(x))) then
            gears @ (GetNeighbourGears input x y)
        else
            let adjacentGears = GetNeighbourGears input x y
            Loop input (x + 1) y (gears @ adjacentGears)
    Loop input (startX - 1) startY []

let ProcessCell (input : string list) (x : int) (y : int) : ((int * int) * int) list =
    if Char.IsNumber (input[y].Chars(x)) && (x - 1 < 0 || not (Char.IsNumber (input[y].Chars(x - 1)))) then
        GetAdjacentGears input x y
        |> List.map (fun g -> (g, (ExtractNumber input[y] x)))
    else
        []
        
let GetGears (input : string list) =
    let rec Loop (input : string list) (x : int) (y : int) (gears : ((int * int) * int) list) = 
        if y = input.Length then
            gears
        else
            let nextX, nextY = CalculateNextXAndY x y input[0].Length
            let processedCellGears = ProcessCell input x y
            Loop input nextX nextY (gears @ processedCellGears)
    Loop input 0 0 []
 
let ReduceGears (gears : ((int * int) * int) list) =
    gears
    |> List.groupBy fst
    |> List.map (snd >> (List.map snd))
    |> List.filter (fun g -> g.Length = 2)
    |> List.map (List.reduce (fun i j -> i * j))
 
let Day3Part2 (input : string list) =
    input
    |> GetGears
    |> ReduceGears
    |> List.sum
    
File.ReadAllLines "input.txt"
|> Array.toList
|> Day3Part2
|> printfn "%i"
    