using Bigschool.DTOs;
using Bigschool.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bigschool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbcontext;
        public FollowingsController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbcontext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exist");
            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _dbcontext.Followings.Add(folowing);
            _dbcontext.SaveChanges();
            return Ok();
        }
    }
}
