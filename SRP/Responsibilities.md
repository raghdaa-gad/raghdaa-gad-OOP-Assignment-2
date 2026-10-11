                                    SRP Violations — Responsibilities Analysis

1. WardBoard

Responsibilities:
Bed Assignment and Patient Tracking, Clinical Acuity Scoring ,Pager Alerting and Logging ,Nurse Handoff Note  ,Census CSV Export

Why This Violates SRP:
WardBoard combines several responsibilities that can change for different reasons, Clinical scoring rules may change independently of pager alert policies, handoff note formatting, or CSV export requirements and Keeping these responsibilities in one class makes the code harder to maintain and test 




2. CheckoutBasket

Responsibilities:
Product Management, Price and Total Calculation, Coupon Parsing, Gift-Wrapping Fee Policy, Gift Message Formatting, Payment Authorization Code Generation

Why This Violates SRP:
CheckoutBasket combines several responsibilities that can change for different reasons. Coupon rules may change independently of pricing calculations, gift message formatting, gift-wrapping fees, or payment authorization code generation. Keeping these responsibilities in one class makes the code harder to maintain and test.



3. SupportTicket

Responsibilities:
Ticket Information Management, Priority Classification, SLA Deadline Calculation, SLA Breach Checking, Public Reply Formatting, Internal Escalation Message Formatting

Why This Violates SRP:
SupportTicket combines several responsibilities that can change for different reasons. Priority classification rules may change independently of SLA policies, public reply formatting, or internal escalation message formatting. Keeping these responsibilities in one class makes the code harder to maintain and test.





4. LoanDesk

Responsibilities:
Risk Score Calculation, Loan Eligibility Assessment, Required Documents Management, Decision Letter Formatting, Underwriter CSV Export

Why This Violates SRP:
LoanDesk combines several responsibilities that can change for different reasons. Risk assessment rules may change independently of document requirements, decision letter formatting, or CSV export requirements. Keeping these responsibilities in one class makes the code harder to maintain and test.



5. CourseEnrollmentDesk

Responsibilities:
Student Registration and Seat Management, Waitlist Management, Welcome Message Formatting, Tuition Invoice Calculation and Formatting, Waitlist Promotion

Why This Violates SRP:
CourseEnrollmentDesk combines several responsibilities that can change for different reasons. Enrollment rules may change independently of welcome message formatting, tuition invoice requirements, or waitlist promotion policies. Keeping these responsibilities in one class makes the code harder to maintain and test.




6. KitchenTicket

Responsibilities:
Menu Item Management, Allergen Detection, Preparation Time Estimation, Thermal Ticket Formatting, Kitchen Lane Assignment

Why This Violates SRP:
KitchenTicket combines several responsibilities that can change for different reasons. Allergen detection rules may change independently of preparation time calculations, thermal ticket formatting, or kitchen lane assignment policies. Keeping these responsibilities in one class makes the code harder to maintain and test.




7. SubscriptionBilling

Responsibilities:
Proration Calculation, Invoice Number Generation, Failed Payment Tracking, Payment Reminder Email Formatting, Accounting Export Formatting

Why This Violates SRP:
SubscriptionBilling combines several responsibilities that can change for different reasons. Proration rules may change independently of invoice numbering schemes, failed payment tracking, reminder email formatting, or accounting export requirements. Keeping these responsibilities in one class makes the code harder to maintain and test.



8. WarehousePickList

Responsibilities:
Pick List Management, Stock Allocation, Warehouse Route Planning, Picker Instruction Formatting, WMS Integration Formatting

Why This Violates SRP:
WarehousePickList combines several responsibilities that can change for different reasons. Stock allocation policies may change independently of warehouse route planning, picker instruction formatting, or WMS integration requirements. Keeping these responsibilities in one class makes the code harder to maintain and test.




9. GradeBook

Responsibilities:
Grade Recording and Validation, Grade Aggregation, Letter Grade Policy, Honor Roll Eligibility Policy, Transcript Formatting, CSV Export Formatting

Why This Violates SRP:
GradeBook combines several responsibilities that can change for different reasons. Grade calculation rules may change independently of letter grade policies, honor roll eligibility criteria, transcript formatting, or CSV export requirements. Keeping these responsibilities in one class makes the code harder to maintain and test.


10. AppointmentDesk

Responsibilities:
Business Hours Policy, Appointment Slot Search, Appointment Booking Management, Calendar Export Formatting, SMS Reminder Formatting, Slot Time Alignment

Why This Violates SRP:
AppointmentDesk combines several responsibilities that can change for different reasons. Business hours policies may change independently of appointment search and booking rules, calendar export formats, or SMS reminder formatting. Keeping these responsibilities in one class makes the code harder to maintain and test.