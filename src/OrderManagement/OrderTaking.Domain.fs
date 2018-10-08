namespace OrderTaking.Domain

open System
open Product
open Quantity
open Customer

module Order =
    type Undefined = exn

    type OrderId = private OrderId of string
    module OrderId = 
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "OrderId must not be null or empty"
            elif str.Length > 50 then
                failwith "OrderId must not be more than 50 chars"
            else
                OrderId str
        let value (OrderId str) = str

    type OrderLineId = OrderLineId of int
    type ShippingAddress = string
    type BillingAddress = string
    type Price = decimal
    type BillingAmount = decimal

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
    
    type UnvalidatedAddress = {
        Lines : string[]
    }

    type CheckedAddress = {
        Lines : string[]
    }

    type CheckProductCodeExists = 
        ProductCode -> bool

    type CheckAddressExists = 
        UnvalidatedAddress -> CheckedAddress

    type ValidatedOrder = {
        OrderId : OrderId
        CustomerInfo : CustomerInfo
        ShippingAddress : CheckedAddress
    }
    type ValidateOrder =
        CheckProductCodeExists -> CheckAddressExists -> UnvalidateOrder -> ValidatedOrder