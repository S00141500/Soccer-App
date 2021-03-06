﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AccaBetApi.Models;
using AccaBetApi.DAL;
using System.Web.Http.Cors;

namespace AccaBetApi.Controllers
{
    [EnableCors(origins: "http://localhost:16901", headers: "*", methods: "*")]
    public class TeamsController : ApiController
    {
        private ITeamsRepository repo;

        public TeamsController()
        {
            repo = new TeamRepository(new AppContext());
        }
        // GET: api/Teams
        public IQueryable<Team> GetTeams()
        {
            return repo.GetAllTeams();
        }

        // GET: api/Teams/5
        [ResponseType(typeof(Team))]
        public IHttpActionResult GetTeam(int id)
        {
            Team team = repo.GetTeamByID(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [Route("api/Teams/{league_id}/teamLeague")]
        [ResponseType(typeof(League))]
        public IHttpActionResult GetLeagueByCountry(int league_id)
        {
            List<Team> team = repo.GetLeagueByCountry(league_id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}