﻿using System;
using System.ComponentModel;

namespace IT008_AppHocAV.Models
{
    public class ListFlashCard
    {
        //       id integer      NOT NULL identity(1,1),
        //user_id integer      NOT NULL,
        //   title      varchar(max) NOT NULL DEFAULT 'untitle',
        //quantity tinyint      NOT NULL,
        //   updated_at datetime NOT NULL,
        //created_at datetime     NOT NULL,
        private int _id;
        private int _userId;
        private string _title;
        private string _description;
        private int _quantity;
        private DateTime _updatedAt;
        private DateTime _createdAt;

        public ListFlashCard()
        {
            this._title = "";
            this._description="";
            this._quantity = 0;
        }
        public ListFlashCard(int id, int userId, string title, string description, int quantity, DateTime updatedAt, DateTime createdAt  )
        {
            _id = id;
            _userId = userId;
            _title = title;
            _description = description;
            this._quantity = quantity;
            this._updatedAt = updatedAt;
            this._createdAt = createdAt;

        }
       

        public int Id
        {
            get => _id;
            set => _id = value;
        }
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }
        public string Title
        {
            get => _title;
            set => _title = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }
        public DateTime UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }


    }
}