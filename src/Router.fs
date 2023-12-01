module Router

[<RequireQualifiedAccess>]
type SiteRoute =
    | Home
    | Counter

[<RequireQualifiedAccess>]
module SiteRoute =
    open Browser

    let getUrl =
        function
        | SiteRoute.Home -> "/"
        | SiteRoute.Counter -> "/counter"

    let tryParseRoute path =

        match path with
        | []
        | [ "" ] -> Some SiteRoute.Home
        | [ "counter" ] -> Some SiteRoute.Counter
        | _ -> None
        |> function
            | Some _ as x -> x
            | None ->
                console.log $"tryParseRoute: {path} failed"
                None

    let getNavTitle =
        function
        | SiteRoute.Home -> "Fable Elmish Lit Tailwind Starter"
        | SiteRoute.Counter -> "Counter"
