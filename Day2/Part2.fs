module Day2Part2

open System.IO
open System.Text.RegularExpressions

let RemoveGame(game : string) =
    Regex.Replace(game, @"Game \d+: ", "")

let MapGames(games : string array) : (int * string) array =
    games
    |> Array.map _.Trim() 
    |> Array.map _.Split(" ")
    |> Array.map (fun game -> (game[0] |> int, game[1]))

let Split(text : string) =
    text.Split [| ';'; ',' |]

let Max (a, b) =
    if fst a > fst b then
        a
    else
        b
        
let GroupColour(pair, name) =
    pair
    |> Array.where (fun colour -> snd colour = name)
    |> Array.map fst
    |> Array.max
    
let GroupColours colours =
    [ GroupColour(colours, "red"); GroupColour(colours, "blue"); GroupColour(colours, "green")]

let ProcessLine text =
    RemoveGame text
    |> Split
    |> MapGames
    |> GroupColours
    |> List.reduce (fun p c -> p * c)
    
let Day2Part2(input : string) : int =
    input.Split "\n"
    |> Array.map ProcessLine
    |> Array.sum
    
let input = File.ReadAllText("input.txt");
printfn $"{Day2Part2(input)}"