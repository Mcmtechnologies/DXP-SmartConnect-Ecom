using DXP.SmartConnect.Ecom.API.Model;
using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.API.Controllers
{
    public class CheckoutController : BaseApiController
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        /// <summary>
        /// Get user checkout info
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <returns>Checkout info detail</returns>
        // GET: ​api/checkout
        [HttpGet]
        public async Task<CheckoutDto> GetCheckoutInfo(string storeId)
        {
            return await _checkoutService.GetCheckoutInfo(storeId);
        }

        /// <summary>
        /// Get available slots page
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <param name="shoppingModeId">Shopping mode</param>
        /// <param name="startPage">start page with 0</param>
        /// <returns>Timeslots page</returns>
        // GET: ​api/checkout/timeslots
        [HttpGet("timeslots")]
        public async Task<TimeSlotsPageDto> GetAvailableSlots(string storeId, string shoppingModeId, int startPage)
        {
            return await _checkoutService.GetAvailableSlots(storeId, shoppingModeId, startPage);
        }

        /// <summary>
        /// Get current reservation slot.
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="uiDateTimeOffsetInMin">Time offset in minutes for UI</param>
        /// <returns>Reservation slot</returns>
        // GET: ​api/checkout/timeslot/reservation
        [HttpGet("timeslot/reservation")]
        public async Task<TimeSlotReservationDto> GetCurrentReservation(string storeId, string uiDateTimeOffsetInMin)
        {
            return await _checkoutService.GetCurrentReservation(storeId, uiDateTimeOffsetInMin);
        }

        /// <summary>
        /// Select time slot
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <param name="reservationSlot">Reservation Slot</param>
        /// <returns>Timeslots page</returns>
        // POST: ​api/checkout/timeslot/select
        [HttpPost("timeslot/select")]
        public async Task<bool> SelectSlot(string storeId, [FromBody] ReservationSlotDto reservationSlot)
        {
            return await _checkoutService.SelectSlot(storeId, reservationSlot?.ReservationId);
        }

        /// <summary>
        /// Get customer payment card
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <returns>Payment card</returns>
        // GET: ​api/checkout/paymentcard
        [HttpGet("paymentcard")]
        public async Task<IList<PaymentCustomerCardDto>> GetPaymentCard(string storeId)
        {
            return await _checkoutService.GetCustomerPaymentCards(storeId);
        }

        /// <summary>
        /// Remove customer payment card
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <param name="cardType">Card type</param>
        /// <param name="cardNumber">start page with 0</param>
        /// <returns>Status</returns>
        // DELETE: ​api/checkout/delete-paymentcard
        [HttpDelete("delete-paymentcard")]
        public async Task<bool> RemovePaymentCard(string storeId, string cardType, string cardNumber)
        {
            return await _checkoutService.RemoveCustomerPaymentCard(storeId, cardType, cardNumber);
        }

        /// <summary>
        /// Add promo code for checkout
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <param name="promoCodeDto">Checkout promo code</param>
        /// <returns>Status</returns>
        // POST: ​api/checkout/add-promocode
        [HttpPost("add-promocode")]
        public async Task<bool> AddPromoCode(string storeId, [FromBody] PromoCodeDto promoCodeDto)
        {
            return await _checkoutService.AddPromoCode(storeId, promoCodeDto?.PromoCode);
        }

        /// <summary>
        /// Add notes for checkout
        /// </summary>
        /// <param name="storeId">Store Number</param>
        /// <param name="notesDto">Checkout notes</param>
        /// <returns>Status</returns>
        // POST: ​api/checkout/add-notes
        [HttpPost("add-notes")]
        public async Task<bool> AddCommnent(string storeId, [FromBody] NotesDto notesDto)
        {
            return await _checkoutService.AddCommnent(storeId, notesDto?.Notes);
        }
    }
}
