using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BaseClass
{
    /// <summary>
    /// Base class for models which allow to create a model from an entity and vice versa
    /// Fields have to be identical between the two objects
    /// </summary>
    public class BaseModel<T_Model, T_Entity>
    {
        public static T_Model EntityToModel(T_Entity entity)
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.CreateMap<T_Entity, T_Model>();
            return AutoMapper.Mapper.Map<T_Entity, T_Model>(entity);
        }

        public static T_Entity ModelToEntity(T_Model model)
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.CreateMap<T_Model, T_Entity>();
            return AutoMapper.Mapper.Map<T_Model, T_Entity>(model);
        }
    }
}