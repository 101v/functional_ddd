namespace OrderTaking.Domain

open System

module Customer =
    type CustomerId = private CustomerId of string
    module CustomerId =
        let create str =
            if String.IsNullOrEmpty(str) then
                failwith "CustomerId must not be null or empty"
            elif str.Length > 100 then
                failwith "CustomerId must not be more than 100 chars"
            else
                CustomerId str
        let value (CustomerId str) = str

    type CustomerInfo = {
        Id : CustomerId
        Name : string
    }
