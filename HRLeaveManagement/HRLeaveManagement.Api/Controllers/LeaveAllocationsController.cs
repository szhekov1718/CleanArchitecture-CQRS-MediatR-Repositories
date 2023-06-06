using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<LeaveAllocationsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
        return Ok(leaveAllocations);
    }

    // GET api/<LeaveAllocationsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailQuery { Id = id });
        return Ok(leaveAllocation);
    }

    // POST api/<LeaveAllocationsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveAllocationsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        return NoContent();
    }

    // DELETE api/<LeaveAllocationsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
        return NoContent();
    }
}