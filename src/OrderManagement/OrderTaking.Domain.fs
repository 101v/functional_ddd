namespace OrderTaking.Domain

open Product
open Quantity

module Order =


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