namespace OrderTaking.Domain

module Order =
    type WidgetCode = private WidgetCode of string
    module WidgetCode =
        let create (code : string) =
            match code with
            | c when c.StartsWith("W") -> Ok(WidgetCode c)
            | c -> Error("Invalid widget code")
        let valud (WidgetCode code) = code
    
    type GizmoCode = GizmoCode of string
    type ProductCode =
        | Widget of WidgetCode
        | Gizmo of GizmoCode

    type UnitQuantity = private UnitQuantity of int
    type KilogramQuantity = KilogramQuantity of decimal
    type OrderQuantity = 
        | Unit of UnitQuantity
        | Kilos of KilogramQuantity

    module UnitQuantity = 
        let create qty =
            if qty < 1 then Error "UnitQuantity can not be negative"
            else if qty > 1000 then Error "UnitQuantity can not be more than 1000"
            else Ok(UnitQuantity qty)
        let value (UnitQuantity qty) = qty

    type Undefined = exn
    type OrderId = Undefined
    type OrderLineId = Undefined
    type CustomerId = Undefined

    type CustomerInfo = Undefined
    type ShippingAddress = Undefined
    type BillingAddress = Undefined
    type Price = Undefined
    type BillingAmount = Undefined

    type NonEmptyList<'a> = {
        First: 'a
        Rest: 'a list
    }

    let l : NonEmptyList<int> = {First = 1; Rest = []}

    type Order = {
        Id : OrderId
        CustomerId : CustomerId
        ShippingAddress : ShippingAddress
        BillingAddress : BillingAddress
        OrderLines : NonEmptyList<OrderLine>
        AmountToBill : BillingAmount
    }

    and OrderLine = {
        Id: OrderLineId
        OrderId: OrderId
        ProductCode : ProductCode
        OrderQuantity: OrderQuantity
        Price: Price
    }

    type UnvalidateOrder = {
        OrderId: string
        CustomerInfo: string
        ShippingAddress: string
    }

    type PlaceOrderEvents = {
        AcknowledgementSent: Undefined
        OrderPlaced: Undefined
        BillingOrderPlaced: Undefined
    }

    type PlaceOrderError = ValidationError of ValidationError list
    and ValidationError = {
        FieldName: string
        ErrorDescription: string
    }

    type PlaceOrder = 
        UnvalidateOrder -> Result<PlaceOrderEvents, PlaceOrderError>
    
    type AddressValidationService = 
        UnvalidateOrder -> PlaceOrderEvents option