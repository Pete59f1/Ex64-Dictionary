using System.Collections.Generic;
using System.Linq;

namespace Dictionary
{
    class MemberController
    {
        private Dictionary<int, Member> actives = new Dictionary<int, Member>();
        private Dictionary<int, Member> passives = new Dictionary<int, Member>();

        public void AddMember(MemberType memberType, Member m)
        {
            //Pre: "m" is a member-object, "m.Id" must not be present in "actives" or "passives"
            //Post: "m" is added to either "actives" or "passives" - which one is determined by "memberType"
            if (!IdAlreadyUsed(m.Id) && !IdAlreadyUsed(m.Id))
            {
                if (memberType == MemberType.active)
                {
                    actives.Add(m.Id, m);
                }
                else
                {
                    passives.Add(m.Id, m);
                }
            }
        }

        public bool IdAlreadyUsed(int id)
        {
            //Pre: None
            //Post: Returns true if "id" is used as "Id" by any object in either "actives" or "passives"
            if (actives.ContainsKey(id) || passives.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Member> Getmembers()
        {
            List<Member> list = new List<Member>();
            foreach (KeyValuePair<int, Member> m in actives)
            {
                list.Add(m.Value);
            }
            foreach (KeyValuePair<int, Member> m in passives)
            {
                list.Add(m.Value);
            }
            //Pre: None
            //Post: A list containing all members from "actives" and "passives" is returned
            return list;
        }

        public bool DeleteMember(int id)
        {
            bool result = false;

            //Pre: None
            //Post: No object having "id" as "Id" exist in either "actives" or "passives"
            //      If an object was removed from either "actives" or "passives" "True" is returned
            //      If no object was removed from either "actives" or "passives" "False" is returned
            if (actives.ContainsKey(id))
            {
                actives.Remove(id);
                result = true;
            }
            else if (passives.ContainsKey(id))
            {
                passives.Remove(id);
                result = true;
            }
            return result;
        }
    }
}