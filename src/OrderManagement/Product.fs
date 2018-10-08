namespace OrderTaking.Domain

module Product = 
    type WidgetCode = private WidgetCode of string
    module WidgetCode =
        let create (code : string) =
            match code with
            | c when c.StartsWith("W") -> Ok(WidgetCode c)
            | _ -> Error("Invalid widget code")
        let valud (WidgetCode code) = code
    
    type GizmoCode = GizmoCode of string

    type ProductCode =
        | Widget of WidgetCode
        | Gizmo of GizmoCode
