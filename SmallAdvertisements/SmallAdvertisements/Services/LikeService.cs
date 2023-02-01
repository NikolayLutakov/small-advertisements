﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallAdvertisements.Data.Context;
using SmallAdvertisements.Data.Entities;
using SmallAdvertisements.Services.Contracts;

namespace SmallAdvertisements.Services
{
    public class LikeService : ILikeService
    {
        private readonly AdvertisementsDbContext _data;

        public LikeService(AdvertisementsDbContext data)
        {
            _data = data;
        }

        public bool Like(int advertisementId, IdentityUser user)
        {
            var advertisementToLike = _data.Advertisements.Where(x => x.Id == advertisementId).Include(x => x.Likes).ThenInclude(x => x.Author).FirstOrDefault();

            if (advertisementToLike == null)
            {
                return false;
            }

            if(advertisementToLike.Likes.Any(a => a.Author.Id == user.Id))
            {
                return false;
            }

            var like = new Like()
            {
                Advertisement = advertisementToLike,
                Author = user
            };

            //advertisementToLike.Likes.Add(like);

            try
            {
                //_data.Advertisements.Update(advertisementToLike);
                _data.Likes.Add(like);
                _data.SaveChanges();
            }
            catch(Exception ex) 
            {
                return false;
            }

            return true;
        }

        public bool Unlike(int advertisementId, IdentityUser user)
        {
            var advertisementToUnLike = _data.Advertisements.FirstOrDefault(x => x.Id == advertisementId);

            if (advertisementToUnLike == null)
            {
                return false;
            }

            var like = _data.Likes.FirstOrDefault(x => x.Author.Id == user.Id && x.AdvertisementId == advertisementId);
            
            if(like == null)
            {
                return false;
            }

            try
            {
                _data.Likes.Remove(like);
                _data.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
